using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using OpenId3as.DivulgacaoONGs.Application.Interfaces.Page;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.Enum;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using System.Collections.Generic;
using System.Linq;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Controllers
{
    [ApiVersionNeutral]
    [Route("api/[controller]")]
    public class CollaboratorPageController : BaseController
    {
        private readonly ICollaboratorAppService _collaboratorPageAppService;
        private readonly IDistributedCache _cache;

        public CollaboratorPageController(IDistributedCache cache,
                                    ICollaboratorAppService collaboratorPageAppService)
        {
            _cache = cache;
            _collaboratorPageAppService = collaboratorPageAppService;
        }

        [HttpGet(Name = "GetAllCollaboratorsPages")]
        [ProducesResponseType(201, Type = typeof(ItemsLinkContainer<CollaboratorViewModel>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ItemsLinkContainer<CollaboratorViewModel> Get()
        {
            var collaborators = _collaboratorPageAppService.GetAll().ToList();
            collaborators.ForEach(x => x.AddRangeLink(CreateLinks(Method.Get, x)));
            var result = new ItemsLinkContainer<CollaboratorViewModel>()
            {
                Items = collaborators
            };
            result.AddRangeLink(CreateLinks(Method.GetAll));
            return result;
        }

        [HttpGet("{id}", Name = "GetCollaboratorPageById")]
        [ProducesResponseType(201, Type = typeof(CollaboratorViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Get(long id)
        {
            var collaborator = _collaboratorPageAppService.GetById(id);
            if (collaborator != null)
            {
                collaborator.AddRangeLink(CreateLinks(Method.Get, collaborator));
                return Ok(collaborator);
            }
            else
                return BadRequest();
        }

        [HttpPost(Name = "InsertCollaboratorPage")]
        [ProducesResponseType(201, Type = typeof(CollaboratorViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public CollaboratorViewModel Post([FromBody]CollaboratorViewModel collaborator)
        {
            collaborator = _collaboratorPageAppService.Add(collaborator);
            collaborator.AddRangeLink(CreateLinks(Method.Post, collaborator));
            return collaborator;
        }

        [HttpPut("{id}", Name = "UpdateCollaboratorPage")]
        [ProducesResponseType(201, Type = typeof(CollaboratorViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Put(long id, [FromBody]CollaboratorViewModel collaborator)
        {
            if (_collaboratorPageAppService.GetById(collaborator.Id).Id != 0)
            {
                collaborator = _collaboratorPageAppService.Update(collaborator);
                collaborator.AddRangeLink(CreateLinks(Method.Put, collaborator));
                return Ok(collaborator);
            }
            else
                return BadRequest();
        }

        [HttpDelete("{id}", Name = "DeleteCollaboratorPage")]
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

        private IEnumerable<Link> CreateLinks(Method method, CollaboratorViewModel collaborator = null)
        {
            var linkContainer = new LinkContainer();
            if (Url != null)
            {
                var getAll = new Link() { Method = "GET", Rel = "get all collaborators pages", Href = Url.Link("GetAllCollaboratorsPages", new { }) };
                var insert = new Link() { Method = "POST", Rel = "insert collaborator page", Href = Url.Link("InsertCollaboratorPage", new { }) };

                var getById = new Link();
                var update = new Link();
                var delete = new Link();

                if (collaborator != null)
                {
                    getById = new Link() { Method = "GET", Rel = "get collaborator page by id", Href = Url.Link("GetCollaboratorPageById", new { id = collaborator.Id }) };
                    update = new Link() { Method = "PUT", Rel = "update collaborator page", Href = Url.Link("UpdateCollaboratorPage", new { id = collaborator.Id }) };
                    delete = new Link() { Method = "DELETE", Rel = "delete collaborator page", Href = Url.Link("DeleteCollaboratorPage", new { id = collaborator.Id }) };
                }

                switch (method)
                {
                    case Method.GetAll:
                        linkContainer.AddLink(getAll);
                        linkContainer.AddLink(insert);
                        break;
                    case Method.Get:
                        linkContainer.AddLink(getById);
                        linkContainer.AddLink(update);
                        linkContainer.AddLink(delete);
                        break;
                    case Method.Post:
                        linkContainer.AddLink(insert);
                        linkContainer.AddLink(getById);
                        linkContainer.AddLink(update);
                        linkContainer.AddLink(delete);
                        break;
                    case Method.Put:
                        linkContainer.AddLink(update);
                        linkContainer.AddLink(getById);
                        linkContainer.AddLink(delete);
                        break;
                }
                linkContainer.Links[0].Rel = "self";
            }
            return linkContainer.Links;
        }
    }
}
