using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using OpenId3as.DivulgacaoONGs.Application.Interfaces;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.Enum;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Collaborators;
using OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.HyperMedia;
using System.Linq;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Controllers
{
    [ApiVersionNeutral]
    [Route("api/[controller]")]
    public class CollaboratorController : BaseController
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
        [ProducesResponseType(201, Type = typeof(ItemsLinkContainer<CollaboratorViewModel>))]
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
        [ProducesResponseType(201, Type = typeof(CollaboratorViewModel))]
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

        [HttpPost(Name = "InsertCollaborator")]
        [ProducesResponseType(201, Type = typeof(CollaboratorViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public CollaboratorViewModel Post([FromBody]CollaboratorViewModel collaborator)
        {
            collaborator = _collaboratorAppService.Add(collaborator);
            collaborator.AddRangeLink(_collaboratorEnricher.CreateLinks(Method.Post, collaborator));
            return collaborator;
        }

        [HttpPut("{id}", Name = "UpdateCollaborator")]
        [ProducesResponseType(201, Type = typeof(CollaboratorViewModel))]
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
