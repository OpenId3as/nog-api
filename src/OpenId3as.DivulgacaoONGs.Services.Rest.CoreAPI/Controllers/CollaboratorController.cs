using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using OpenId3as.DivulgacaoONGs.Application.Interfaces;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.Enum;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Collaborators;
using System;
using System.Collections.Generic;
using System.Linq;
using IPageApp = OpenId3as.DivulgacaoONGs.Application.Interfaces.Page;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Controllers
{
    [ApiVersionNeutral]
    [Route("api/[controller]")]
    public class CollaboratorController : BaseController
    {
        private readonly ICollaboratorAppService _collaboratorAppService;
        private readonly IPageApp.ICollaboratorAppService _collaboratorPageAppService;
        private readonly IDistributedCache _cache;

        public CollaboratorController(IDistributedCache cache,
            ICollaboratorAppService collaboratorAppService,
            IPageApp.ICollaboratorAppService collaboratorPageAppService
            )
        {
            _cache = cache;
            _collaboratorAppService = collaboratorAppService;
            _collaboratorPageAppService = collaboratorPageAppService;
        }

        [HttpGet(Name = "GetAllCollaborators")]
        [ProducesResponseType(201, Type = typeof(ItemsLinkContainer<CollaboratorViewModel>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ItemsLinkContainer<CollaboratorViewModel> Get()
        {
            var collaborators = _collaboratorAppService.GetAll().ToList();
            collaborators.ForEach(x => x.AddRangeLink(CreateLinks(Method.Get, x)));
            var result = new ItemsLinkContainer<CollaboratorViewModel>()
            {
                Items = collaborators
            };
            result.AddRangeLink(CreateLinks(Method.GetAll));
            return result;
        }

        /// <summary>Busca o colaborador da ONG por Id.</summary>
        /// <param name="id">Id do colaborador.</param>
        /// <returns>Objeto contendo todas as informações do colaborador.</returns>
        [HttpGet("{id}", Name = "GetCollaboratorById")]
        public CollaboratorViewModel Get(Int64 id)
        {
            var collaborator = _collaboratorAppService.GetById(id);
            collaborator.AddRangeLink(CreateLinks(Method.Get, collaborator));
            return collaborator;
        }

        [HttpPost(Name = "InsertCollaborator")]
        public CollaboratorViewModel Post([FromBody]CollaboratorViewModel collaborator)
        {
            collaborator = _collaboratorAppService.Add(collaborator);
            collaborator.AddRangeLink(CreateLinks(Method.Post, collaborator));
            return collaborator;
        }

        [HttpPut("{id}", Name = "UpdateCollaborator")]
        public CollaboratorViewModel Put(Int64 id, [FromBody]CollaboratorViewModel collaborator)
        {
            collaborator = _collaboratorAppService.Update(collaborator);
            collaborator.AddRangeLink(CreateLinks(Method.Put, collaborator));
            return collaborator;
        }

        [HttpDelete("{id}", Name = "DeleteCollaboratorRoute")]
        public void Delete(Int64 id)
        {
            _collaboratorAppService.Delete(id);
        }

        private IEnumerable<Link> CreateLinks(Method method, CollaboratorViewModel collaborator = null)
        {
            var linkContainer = new LinkContainer();
            var getAll = new Link() { Method = "GET", Rel = "get all collaborators", Href = Url.Link("GetAllCollaborators", new { }) };
            var getById = new Link() { Method = "GET", Rel = "get collaborator by id", Href = Url.Link("GetCollaboratorById", new { id = collaborator.Id }) };
            var insert = new Link() { Method = "POST", Rel = "insert collaborator", Href = Url.Link("InsertCollaborator", new { }) };
            var update = new Link() { Method = "PUT", Rel = "update collaborator", Href = Url.Link("UpdateCollaborator", new { id = collaborator.Id }) };
            var delete = new Link() { Method = "DELETE", Rel = "delete collaborator", Href = Url.Link("DeleteCollaboratorRoute", new { id = collaborator.Id }) };
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
                case Method.Delete:
                    linkContainer.AddLink(delete);
                    linkContainer.AddLink(getById);
                    linkContainer.AddLink(update);
                    break;
            }
            linkContainer.Links[0].Rel = "self";
            return linkContainer.Links;
        }
    }
}
