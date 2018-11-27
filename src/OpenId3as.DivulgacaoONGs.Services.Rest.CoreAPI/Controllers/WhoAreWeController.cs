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
    public class WhoAreWeController : Controller
    {
        private readonly IWhoAreWeAppService _whoAreWeAppService;
        private readonly IDistributedCache _cache;
        public WhoAreWeController(IDistributedCache cache, IWhoAreWeAppService whoAreWeAppService)
        {
            _cache = cache;
            _whoAreWeAppService = whoAreWeAppService;
        }

        [HttpGet]
        public IEnumerable<WhoAreWeViewModel> Get()
        {
            return _whoAreWeAppService.GetAll();
        }

        [HttpGet("{id}")]
        public WhoAreWeViewModel Get(Int64 id)
        {
            return _whoAreWeAppService.GetById(id);
        }

        [HttpPost]
        public WhoAreWeViewModel Post([FromBody]WhoAreWeViewModel whoAreWe)
        {
            return _whoAreWeAppService.Add(whoAreWe);
        }

        [HttpPut("{id}")]
        public WhoAreWeViewModel Put(Int64 id, [FromBody]WhoAreWeViewModel whoAreWe)
        {
            return _whoAreWeAppService.Update(whoAreWe);
        }

        [HttpDelete("{id}")]
        public void Delete(Int64 id)
        {
            _whoAreWeAppService.Delete(id);
        }
    }
}
