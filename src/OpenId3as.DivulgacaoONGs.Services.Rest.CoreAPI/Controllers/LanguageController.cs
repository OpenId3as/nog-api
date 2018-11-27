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
    public class LanguageController : Controller
    {
        private readonly ILanguageAppService _languageAppService;
        private readonly IDistributedCache _cache;
        public LanguageController(IDistributedCache cache, ILanguageAppService languageAppService)
        {
            _cache = cache;
            _languageAppService = languageAppService;
        }

        [HttpGet]
        public IEnumerable<LanguageViewModel> Get()
        {
            return _languageAppService.GetAll();
        }

        [HttpGet("{id}")]
        public LanguageViewModel Get(Int64 id)
        {
            return _languageAppService.GetById(id);
        }

        [HttpPost]
        public LanguageViewModel Post([FromBody]LanguageViewModel language)
        {
            return _languageAppService.Add(language);
        }

        [HttpPut("{id}")]
        public LanguageViewModel Put(Int64 id, [FromBody]LanguageViewModel language)
        {
            return _languageAppService.Update(language);
        }

        [HttpDelete("{id}")]
        public void Delete(Int64 id)
        {
            _languageAppService.Delete(id);
        }
    }
}
