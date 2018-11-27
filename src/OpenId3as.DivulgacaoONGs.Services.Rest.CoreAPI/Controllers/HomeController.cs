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
    public class HomeController : Controller
    {
        private readonly IHomeAppService _homeAppService;
        private readonly IDistributedCache _cache;
        public HomeController(IDistributedCache cache, IHomeAppService homeAppService)
        {
            _cache = cache;
            _homeAppService = homeAppService;
        }

        [HttpGet]
        public IEnumerable<HomeViewModel> Get()
        {
            return _homeAppService.GetAll();
        }

        [HttpGet("{id}")]
        public HomeViewModel Get(Int64 id)
        {
            return _homeAppService.GetById(id);
        }

        [HttpPost]
        public HomeViewModel Post([FromBody]HomeViewModel home)
        {
            return _homeAppService.Add(home);
        }

        [HttpPut("{id}")]
        public HomeViewModel Put(Int64 id, [FromBody]HomeViewModel home)
        {
            return _homeAppService.Update(home);
        }

        [HttpDelete("{id}")]
        public void Delete(Int64 id)
        {
            _homeAppService.Delete(id);
        }
    }
}
