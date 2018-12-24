using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using OpenId3as.DivulgacaoONGs.Application.Interfaces.Page;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.Enum;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Security;
using OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.HyperMedia;
using System.Linq;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Controllers
{
    [ApiVersionNeutral]
    [Route("api/[controller]")]
    [Authorize]
    public class LogoController : BaseController
    {
        private readonly ILogoAppService _logoAppService;
        private readonly IDistributedCache _cache;
        private readonly LogoEnricher _logoEnricher;

        public LogoController(IDistributedCache cache, ILogoAppService logoAppService, IUrlHelper urlHelper)
        {
            _cache = cache;
            _logoAppService = logoAppService;
            _logoEnricher = new LogoEnricher(urlHelper);
        }

        [HttpGet(Name = "GetAllLogos")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(ItemsLinkContainer<LogoViewModel>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ItemsLinkContainer<LogoViewModel> Get()
        {
            var logos = _logoAppService.GetAll().ToList();
            logos.ForEach(x => x.AddRangeLink(_logoEnricher.CreateLinks(Method.Get, x)));
            var result = new ItemsLinkContainer<LogoViewModel>()
            {
                Items = logos
            };
            result.AddRangeLink(_logoEnricher.CreateLinks(Method.GetAll));
            return result;
        }

        [HttpGet("{id}", Name = "GetLogoById")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(LogoViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Get(long id)
        {
            var logo = _logoAppService.GetById(id);
            if (logo != null)
            {
                logo.AddRangeLink(_logoEnricher.CreateLinks(Method.Get, logo));
                return Ok(logo);
            }
            else
                return BadRequest();
        }

        [HttpPost(Name = "InsertLogo")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(LogoViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public LogoViewModel Post([FromBody]LogoViewModel logo)
        {
            logo = _logoAppService.Add(logo);
            logo.AddRangeLink(_logoEnricher.CreateLinks(Method.Post, logo));
            return logo;
        }

        [HttpPut("{id}", Name = "UpdateLogo")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(LogoViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Put(int id, [FromBody]LogoViewModel logo)
        {
            if (_logoAppService.GetById(logo.Id).Id != 0)
            {
                logo = _logoAppService.Update(logo);
                logo.AddRangeLink(_logoEnricher.CreateLinks(Method.Put, logo));
                return Ok(logo);
            }
            else
                return BadRequest();
        }

        [HttpDelete("{id}", Name = "DeleteLogo")]
        [Authorize(AuthorizationNameOptions.Bearer)]
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
    }
}
