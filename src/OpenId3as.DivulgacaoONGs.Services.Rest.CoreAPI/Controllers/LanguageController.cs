using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using OpenId3as.DivulgacaoONGs.Application.Interfaces.Page;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.Enum;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using System.Collections.Generic;
using System.Linq;

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

        [HttpGet(Name = "GetAllLanguages")]
        [ProducesResponseType(201, Type = typeof(ItemsLinkContainer<LanguageViewModel>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ItemsLinkContainer<LanguageViewModel> Get()
        {
            var languages = _languageAppService.GetAll().ToList();
            languages.ForEach(x => x.AddRangeLink(CreateLinks(Method.Get, x)));
            var result = new ItemsLinkContainer<LanguageViewModel>()
            {
                Items = languages
            };
            result.AddRangeLink(CreateLinks(Method.GetAll));
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
                language.AddRangeLink(CreateLinks(Method.Get, language));
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
            language.AddRangeLink(CreateLinks(Method.Post, language));
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
                language.AddRangeLink(CreateLinks(Method.Put, language));
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

        private IEnumerable<Link> CreateLinks(Method method, LanguageViewModel language = null)
        {
            var linkContainer = new LinkContainer();
            if (Url != null)
            {
                var getAll = new Link() { Method = "GET", Rel = "get all languages", Href = Url.Link("GetAllLanguages", new { }) };
                var insert = new Link() { Method = "POST", Rel = "insert language", Href = Url.Link("InsertLanguage", new { }) };

                var getById = new Link();
                var update = new Link();
                var delete = new Link();

                if (language != null)
                {
                    getById = new Link() { Method = "GET", Rel = "get language by id", Href = Url.Link("GetLanguageById", new { id = language.Id }) };
                    update = new Link() { Method = "PUT", Rel = "update language", Href = Url.Link("UpdateLanguage", new { id = language.Id }) };
                    delete = new Link() { Method = "DELETE", Rel = "delete language", Href = Url.Link("DeleteLanguage", new { id = language.Id }) };
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
