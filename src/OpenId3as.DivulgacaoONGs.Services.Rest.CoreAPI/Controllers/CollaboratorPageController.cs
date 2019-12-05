using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using OpenId3as.DivulgacaoONGs.Application.Interfaces.Page;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.Enum;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Security;
using OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.HyperMedia;
using System.Linq;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Controllers
{
    [ApiVersionNeutral]
    [Route("api/[controller]")]
    [Authorize]
    public class CollaboratorPageController : ControllerBase
    {
        private readonly ICollaboratorAppService _collaboratorPageAppService;
        private readonly IDistributedCache _cache;
        private readonly CollaboratorPageEnricher _collaboratorPageEnricher;

        public CollaboratorPageController(
            IDistributedCache cache,
            ICollaboratorAppService collaboratorPageAppService,
            IUrlHelper urlHelper)
        {
            _cache = cache;
            _collaboratorPageAppService = collaboratorPageAppService;
            _collaboratorPageEnricher = new CollaboratorPageEnricher(urlHelper);
        }

        [HttpGet(Name = "GetAllCollaboratorsPages")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(ItemsLinkContainer<CollaboratorViewModel>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ItemsLinkContainer<CollaboratorViewModel> Get()
        {
            var collaborators = _collaboratorPageAppService.GetAll().ToList();
            collaborators.ForEach(x => x.AddRangeLink(_collaboratorPageEnricher.CreateLinks(Method.Get, x)));
            var result = new ItemsLinkContainer<CollaboratorViewModel>()
            {
                Items = collaborators
            };
            result.AddRangeLink(_collaboratorPageEnricher.CreateLinks(Method.GetAll));
            return result;
        }

        [HttpGet("{id}", Name = "GetCollaboratorPageById")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(CollaboratorViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Get(long id)
        {
            var collaborator = _collaboratorPageAppService.GetById(id);
            if (collaborator != null)
            {
                collaborator.AddRangeLink(_collaboratorPageEnricher.CreateLinks(Method.Get, collaborator));
                return Ok(collaborator);
            }
            else
                return BadRequest();
        }

        [HttpPost(Name = "InsertCollaboratorPage")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(CollaboratorViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public CollaboratorViewModel Post([FromBody]CollaboratorViewModel collaborator)
        {
            collaborator = _collaboratorPageAppService.Add(collaborator);
            collaborator.AddRangeLink(_collaboratorPageEnricher.CreateLinks(Method.Post, collaborator));
            return collaborator;
        }

        [HttpPut("{id}", Name = "UpdateCollaboratorPage")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(CollaboratorViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Put(long id, [FromBody]CollaboratorViewModel collaborator)
        {
            if (_collaboratorPageAppService.GetById(collaborator.Id).Id != 0)
            {
                collaborator = _collaboratorPageAppService.Update(collaborator);
                collaborator.AddRangeLink(_collaboratorPageEnricher.CreateLinks(Method.Put, collaborator));
                return Ok(collaborator);
            }
            else
                return BadRequest();
        }

        [HttpDelete("{id}", Name = "DeleteCollaboratorPage")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Delete(long id)
        {
            if (_collaboratorPageAppService.GetById(id).Id != 0)
            {
                _collaboratorPageAppService.Delete(id);
                return NoContent();
            }
            else
                return BadRequest();
        }
    }
}
