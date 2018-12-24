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
    public class HomeController : Controller
    {
        private readonly IHomeAppService _homeAppService;
        private readonly IDistributedCache _cache;
        private readonly HomeEnricher _homeEnricher;

        public HomeController(IDistributedCache cache, IHomeAppService homeAppService, IUrlHelper urlHelper)
        {
            _cache = cache;
            _homeAppService = homeAppService;
            _homeEnricher = new HomeEnricher(urlHelper);
        }

        [HttpGet(Name = "GetAllHomes")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(ItemsLinkContainer<HomeViewModel>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ItemsLinkContainer<HomeViewModel> Get()
        {
            var obj = _homeAppService.GetAll().ToList();
            obj.ForEach(x => x.AddRangeLink(_homeEnricher.CreateLinks(Method.Get, x)));
            var result = new ItemsLinkContainer<HomeViewModel>()
            {
                Items = obj
            };
            result.AddRangeLink(_homeEnricher.CreateLinks(Method.GetAll));
            return result;
        }

        [HttpGet("{id}", Name = "GetHomeById")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(HomeViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Get(long id)
        {
            var home = _homeAppService.GetById(id);
            if (home != null)
            {
                home.AddRangeLink(_homeEnricher.CreateLinks(Method.Get, home));
                return Ok(home);
            }
            else
                return BadRequest();
        }

        [HttpPost(Name = "InsertHome")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(HomeViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public HomeViewModel Post([FromBody]HomeViewModel home)
        {
            home = _homeAppService.Add(home);
            home.AddRangeLink(_homeEnricher.CreateLinks(Method.Post, home));
            return home;
        }

        [HttpPut("{id}", Name = "UpdateHome")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(HomeViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Put(long id, [FromBody]HomeViewModel home)
        {
            if (_homeAppService.GetById(home.Id).Id != 0)
            {
                home = _homeAppService.Update(home);
                home.AddRangeLink(_homeEnricher.CreateLinks(Method.Put, home));
                return Ok(home);
            }
            else
                return BadRequest();
        }

        [HttpDelete("{id}", Name = "DeleteHome")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Delete(long id)
        {
            if (_homeAppService.GetById(id).Id != 0)
            {
                _homeAppService.Delete(id);
                return NoContent();
            }
            else
                return BadRequest();
        }
    }
}
