using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using OpenId3as.DivulgacaoONGs.Application.Interfaces;
using IPageApp = OpenId3as.DivulgacaoONGs.Application.Interfaces.Page;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Collaborators;
using PageVM = OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Controllers
{
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

        [HttpGet]
        public IEnumerable<CollaboratorViewModel> Get()
        {
            return _collaboratorAppService.GetAll();
        }

        /// <summary>Busca o colaborador da ONG por Id.</summary>
        /// <param name="id">Id do colaborador.</param>
        /// <returns>Objeto contendo todas as informações do colaborador.</returns>
        [HttpGet("{id}")]
        public CollaboratorViewModel Get(Int64 id)
        {
            return _collaboratorAppService.GetById(id);
        }

        [HttpPost]
        public CollaboratorViewModel Post([FromBody]CollaboratorViewModel collaborator)
        {
            return _collaboratorAppService.Add(collaborator);
        }

        [HttpPut("{id}")]
        public CollaboratorViewModel Put(Int64 id, [FromBody]CollaboratorViewModel collaborator)
        {
            return _collaboratorAppService.Update(collaborator);
        }

        [HttpDelete("{id}")]
        public void Delete(Int64 id)
        {
            _collaboratorAppService.Delete(id);
        }
    }
}
