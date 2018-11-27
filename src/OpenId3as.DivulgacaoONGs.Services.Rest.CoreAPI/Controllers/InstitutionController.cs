using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using OpenId3as.DivulgacaoONGs.Application.Interfaces;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Institutions;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Controllers
{
    [ApiVersionNeutral]
    [Route("api/[controller]")]
    public class InstitutionController : BaseController
    {
        private readonly IInstitutionAppService _institutionAppService;
        private readonly IDistributedCache _cache;
        public InstitutionController(IDistributedCache cache, IInstitutionAppService institutionAppService)
        {
            _cache = cache;
            _institutionAppService = institutionAppService;
        }

        [HttpGet]
        public IEnumerable<InstitutionViewModel> Get()
        {
            return _institutionAppService.GetAll();
        }

        [HttpGet("{id}")]
        public InstitutionViewModel Get(Int64 id)
        {
            return _institutionAppService.GetById(id);
        }

        [HttpPost]
        public InstitutionViewModel Post([FromBody]InstitutionViewModel institution)
        {
            return _institutionAppService.Add(institution);
        }

        [HttpPut("{id}")]
        public InstitutionViewModel Put(Int64 id, [FromBody]InstitutionViewModel institution)
        {
            return _institutionAppService.Update(institution);
        }

        [HttpDelete("{id}")]
        public void Delete(Int64 id)
        {
            _institutionAppService.Delete(id);
        }
    }
}
