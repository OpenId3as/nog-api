using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using OpenId3as.DivulgacaoONGs.Application.Interfaces.Page;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Controllers
{
    [ApiVersionNeutral]
    [Route("api/[controller]")]
    public class LogoController : BaseController
    {
        private readonly ILogoAppService _logoAppService;
        private readonly IDistributedCache _cache;
        public LogoController(IDistributedCache cache, ILogoAppService logoAppService)
        {
            _cache = cache;
            _logoAppService = logoAppService;
        }

        [HttpGet]
        public IEnumerable<LogoViewModel> Get()
        {
            return _logoAppService.GetAll();
        }

        [HttpGet("{id}")]
        public LogoViewModel Get(Int64 id)
        {
            return _logoAppService.GetById(id);
        }

        [HttpPost]
        public LogoViewModel Post([FromBody]LogoViewModel logo)
        {
            return _logoAppService.Add(logo);
        }

        [HttpPut("{id}")]
        public LogoViewModel Put(int id, [FromBody]LogoViewModel logo)
        {
            return _logoAppService.Update(logo);
        }

        [HttpDelete("{id}")]
        public void Delete(Int64 id)
        {
            _logoAppService.Delete(id);
        }
    }
}
