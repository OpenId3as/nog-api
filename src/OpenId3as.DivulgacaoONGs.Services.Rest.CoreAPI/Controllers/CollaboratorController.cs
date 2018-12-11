﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using OpenId3as.DivulgacaoONGs.Application.Interfaces;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.Enum;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Collaborators;
using System.Collections.Generic;
using System.Linq;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Controllers
{
    [ApiVersionNeutral]
    [Route("api/[controller]")]
    public class CollaboratorController : BaseController
    {
        private readonly ICollaboratorAppService _collaboratorAppService;
        private readonly IDistributedCache _cache;

        public CollaboratorController(IDistributedCache cache,
                                    ICollaboratorAppService collaboratorAppService)
        {
            _cache = cache;
            _collaboratorAppService = collaboratorAppService;
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
                collaborator.AddRangeLink(CreateLinks(Method.Get, collaborator));
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
            collaborator.AddRangeLink(CreateLinks(Method.Post, collaborator));
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
                collaborator.AddRangeLink(CreateLinks(Method.Put, collaborator));
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

        private IEnumerable<Link> CreateLinks(Method method, CollaboratorViewModel collaborator = null)
        {
            var linkContainer = new LinkContainer();
            if(Url != null)
            {
                var getAll = new Link() { Method = "GET", Rel = "get all collaborators", Href = Url.Link("GetAllCollaborators", new { }) };
                var insert = new Link() { Method = "POST", Rel = "insert collaborator", Href = Url.Link("InsertCollaborator", new { }) };

                var getById = new Link();
                var update = new Link();
                var delete = new Link();

                if (collaborator != null)
                {
                    getById = new Link() { Method = "GET", Rel = "get collaborator by id", Href = Url.Link("GetCollaboratorById", new { id = collaborator.Id }) };
                    update = new Link() { Method = "PUT", Rel = "update collaborator", Href = Url.Link("UpdateCollaborator", new { id = collaborator.Id }) };
                    delete = new Link() { Method = "DELETE", Rel = "delete collaborator", Href = Url.Link("DeleteCollaborator", new { id = collaborator.Id }) };
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
