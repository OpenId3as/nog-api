using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using OpenId3as.DivulgacaoONGs.Application.Interfaces.Page;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.Enum;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.HyperMedia;
using System.Linq;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Controllers
{
    [ApiVersionNeutral]
    [Route("api/[controller]")]
    public class LanguageController : Controller
    {
        private readonly ILanguageAppService _languageAppService;
        private readonly IDistributedCache _cache;
        private readonly LanguageEnricher _languageEnricher;

        public LanguageController(IDistributedCache cache, ILanguageAppService languageAppService, IUrlHelper urlHelper)
        {
            _cache = cache;
            _languageAppService = languageAppService;
            _languageEnricher = new LanguageEnricher(urlHelper);
        }

        [HttpGet(Name = "GetAllLanguages")]
        [ProducesResponseType(201, Type = typeof(ItemsLinkContainer<LanguageViewModel>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ItemsLinkContainer<LanguageViewModel> Get()
        {
            var languages = _languageAppService.GetAll().ToList();
            languages.ForEach(x => x.AddRangeLink(_languageEnricher.CreateLinks(Method.Get, x)));
            var result = new ItemsLinkContainer<LanguageViewModel>()
            {
                Items = languages
            };
            result.AddRangeLink(_languageEnricher.CreateLinks(Method.GetAll));
            return result;
        }

        [HttpGet("{id}", Name = "GetLanguageById")]
        [ProducesResponseType(201, Type = typeof(LanguageViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Get(long id)
        {
            var language = _languageAppService.GetById(id);
            if (language != null)
            {
                language.AddRangeLink(_languageEnricher.CreateLinks(Method.Get, language));
                return Ok(language);
            }
            else
                return BadRequest();
        }

        [HttpPost(Name = "InsertLanguage")]
        [ProducesResponseType(201, Type = typeof(LanguageViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public LanguageViewModel Post([FromBody]LanguageViewModel language)
        {
            language = _languageAppService.Add(language);
            language.AddRangeLink(_languageEnricher.CreateLinks(Method.Post, language));
            return language;
        }

        [HttpPut("{id}", Name = "UpdateLanguage")]
        [ProducesResponseType(201, Type = typeof(LanguageViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Put(long id, [FromBody]LanguageViewModel language)
        {
            if (_languageAppService.GetById(language.Id).Id != 0)
            {
                language = _languageAppService.Update(language);
                language.AddRangeLink(_languageEnricher.CreateLinks(Method.Put, language));
                return Ok(language);
            }
            else
                return BadRequest();
        }

        [HttpDelete("{id}", Name = "DeleteLanguage")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Delete(long id)
        {
            if (_languageAppService.GetById(id).Id != 0)
            {
                _languageAppService.Delete(id);
                return NoContent();
            }
            else
                return BadRequest();
        }
    }
}
