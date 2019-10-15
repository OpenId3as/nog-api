using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using OpenId3as.DivulgacaoONGs.Application.DataTransferObject.PagedSearch;
using OpenId3as.DivulgacaoONGs.Application.Interfaces;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.Enum;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Collaborators;
using OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Security;
using OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.HyperMedia;
using System.Collections.Generic;
using System.Linq;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Controllers
{
    [ApiVersionNeutral]
    [Route("api/[controller]")]
    [Authorize]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorAppService _collaboratorAppService;
        private readonly IDistributedCache _cache;
        private readonly CollaboratorEnricher _collaboratorEnricher;

        public CollaboratorController(IDistributedCache cache,
                                    ICollaboratorAppService collaboratorAppService,
                                    IUrlHelper urlHelper)
        {
            _cache = cache;
            _collaboratorAppService = collaboratorAppService;
            _collaboratorEnricher = new CollaboratorEnricher(urlHelper);
        }

        [HttpGet(Name = "GetAllCollaborators")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(ItemsLinkContainer<CollaboratorViewModel>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ItemsLinkContainer<CollaboratorViewModel> Get()
        {
            var collaborators = _collaboratorAppService.GetAll().ToList();
            collaborators.ForEach(x => x.AddRangeLink(_collaboratorEnricher.CreateLinks(Method.Get, x)));
            var result = new ItemsLinkContainer<CollaboratorViewModel>()
            {
                Items = collaborators
            };
            result.AddRangeLink(_collaboratorEnricher.CreateLinks(Method.GetAll));
            return result;
        }

        /// <summary>Busca o colaborador da ONG por Id.</summary>
        /// <param name="id">Id do colaborador.</param>
        /// <returns>Objeto contendo todas as informações do colaborador.</returns>
        [HttpGet("{id}", Name = "GetCollaboratorById")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(CollaboratorViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Get(long id)
        {
            var collaborator = _collaboratorAppService.GetById(id);
            if (collaborator != null)
            {
                collaborator.AddRangeLink(_collaboratorEnricher.CreateLinks(Method.Get, collaborator));
                return Ok(collaborator);
            }
            else
                return BadRequest();
        }

        [HttpGet("FindWithPagedSearch/{limitRows}/{page}", Name = "GetCollaboratorPagedSearch")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(PagedSearch<CollaboratorViewModel>))]
        [ProducesResponseType(401)]
        public IActionResult FindWithPagedSearch(int limitRows, int page, [FromQuery] List<SortItemDTO> sort,
            [FromQuery] string firstName, [FromQuery] string lastName, [FromQuery] string email, [FromQuery] bool? active)
        {
            sort = sort ?? new List<SortItemDTO>();
            var pagedSearch = _collaboratorAppService.FindWithPagedSearch(sort, limitRows, page, firstName, lastName, email, active);
            foreach (var collaborator in pagedSearch.List)
                collaborator.AddRangeLink(_collaboratorEnricher.CreateLinks(Method.Get, collaborator));
            return Ok(pagedSearch);
        }

        [HttpPost(Name = "InsertCollaborator")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(CollaboratorViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public CollaboratorViewModel Post([FromBody]CollaboratorViewModel collaborator)
        {
            collaborator = _collaboratorAppService.Add(collaborator);
            collaborator.AddRangeLink(_collaboratorEnricher.CreateLinks(Method.Post, collaborator));
            return collaborator;
        }

        [HttpPut("{id}", Name = "UpdateCollaborator")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(CollaboratorViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Put(long id, [FromBody]CollaboratorViewModel collaborator)
        {
            if (_collaboratorAppService.GetById(collaborator.Id).Id != 0)
            {
                collaborator = _collaboratorAppService.Update(collaborator);
                collaborator.AddRangeLink(_collaboratorEnricher.CreateLinks(Method.Put, collaborator));
                return Ok(collaborator);
            }
            else
                return BadRequest();
        }

        [HttpDelete("{id}", Name = "DeleteCollaborator")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Delete(long id)
        {
            if (_collaboratorAppService.GetById(id).Id != 0)
            {
                _collaboratorAppService.Delete(id);
                return NoContent();
            }
            else
                return BadRequest();
        }
    }
}
