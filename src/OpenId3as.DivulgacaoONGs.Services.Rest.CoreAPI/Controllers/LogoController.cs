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
    public class LogoController : BaseController
    {
        private readonly ILogoAppService _logoAppService;
        private readonly IDistributedCache _cache;
        public LogoController(IDistributedCache cache, ILogoAppService logoAppService)
        {
            _cache = cache;
            _logoAppService = logoAppService;
        }

        [HttpGet(Name = "GetAllLogos")]
        [ProducesResponseType(201, Type = typeof(ItemsLinkContainer<LogoViewModel>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ItemsLinkContainer<LogoViewModel> Get()
        {
            var logos = _logoAppService.GetAll().ToList();
            logos.ForEach(x => x.AddRangeLink(CreateLinks(Method.Get, x)));
            var result = new ItemsLinkContainer<LogoViewModel>()
            {
                Items = logos
            };
            result.AddRangeLink(CreateLinks(Method.GetAll));
            return result;
        }

        [HttpGet("{id}", Name = "GetLogoById")]
        [ProducesResponseType(201, Type = typeof(LogoViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Get(long id)
        {
            var logo = _logoAppService.GetById(id);
            if (logo != null)
            {
                logo.AddRangeLink(CreateLinks(Method.Get, logo));
                return Ok(logo);
            }
            else
                return BadRequest();
        }

        [HttpPost(Name = "InsertLogo")]
        [ProducesResponseType(201, Type = typeof(LogoViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public LogoViewModel Post([FromBody]LogoViewModel logo)
        {
            logo = _logoAppService.Add(logo);
            logo.AddRangeLink(CreateLinks(Method.Post, logo));
            return logo;
        }

        [HttpPut("{id}", Name = "UpdateLogo")]
        [ProducesResponseType(201, Type = typeof(LogoViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Put(int id, [FromBody]LogoViewModel logo)
        {
            if (_logoAppService.GetById(logo.Id).Id != 0)
            {
                logo = _logoAppService.Update(logo);
                logo.AddRangeLink(CreateLinks(Method.Put, logo));
                return Ok(logo);
            }
            else
                return BadRequest();
        }

        [HttpDelete("{id}", Name = "DeleteLogo")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Delete(long id)
        {
            if (_logoAppService.GetById(id).Id != 0)
            {
                _logoAppService.Delete(id);
                return NoContent();
            }
            else
                return BadRequest();
        }

        private IEnumerable<Link> CreateLinks(Method method, LogoViewModel logo = null)
        {
            var linkContainer = new LinkContainer();
            if (Url != null)
            {
                var getAll = new Link() { Method = "GET", Rel = "get all logos", Href = Url.Link("GetAllLogos", new { }) };
                var insert = new Link() { Method = "POST", Rel = "insert logo", Href = Url.Link("InsertLogo", new { }) };

                var getById = new Link();
                var update = new Link();
                var delete = new Link();

                if (logo != null)
                {
                    getById = new Link() { Method = "GET", Rel = "get logo by id", Href = Url.Link("GetLogoById", new { id = logo.Id }) };
                    update = new Link() { Method = "PUT", Rel = "update logo", Href = Url.Link("UpdateLogo", new { id = logo.Id }) };
                    delete = new Link() { Method = "DELETE", Rel = "delete logo", Href = Url.Link("DeleteLogo", new { id = logo.Id }) };
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
