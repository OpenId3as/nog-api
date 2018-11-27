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
    public class HowToHelpController : Controller
    {
        private readonly IHowToHelpAppService _howToHelpAppService;
        private readonly IDistributedCache _cache;
        public HowToHelpController(IDistributedCache cache, IHowToHelpAppService howToHelpAppService)
        {
            _cache = cache;
            _howToHelpAppService = howToHelpAppService;
        }

        [HttpGet]
        public IEnumerable<HowToHelpViewModel> Get()
        {
            return _howToHelpAppService.GetAll();
        }

        [HttpGet("{id}")]
        public HowToHelpViewModel Get(Int64 id)
        {
            return _howToHelpAppService.GetById(id);
        }

        [HttpPost]
        public HowToHelpViewModel Post([FromBody]HowToHelpViewModel howToHelp)
        {
            return _howToHelpAppService.Add(howToHelp);
        }

        [HttpPut("{id}")]
        public HowToHelpViewModel Put(Int64 id, [FromBody]HowToHelpViewModel howToHelp)
        {
            return _howToHelpAppService.Update(howToHelp);
        }

        [HttpDelete("{id}")]
        public void Delete(Int64 id)
        {
            _howToHelpAppService.Delete(id);
        }
    }
}
