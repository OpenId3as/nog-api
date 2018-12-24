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
    public class BannerController : BaseController
    {
        private readonly IBannerAppService _bannerAppService;
        private readonly IDistributedCache _cache;
        private readonly BannerEnricher _bannerEnricher;

        public BannerController(IDistributedCache cache, IBannerAppService bannerAppService, IUrlHelper urlHelper)
        {
            _cache = cache;
            _bannerAppService = bannerAppService;
            _bannerEnricher = new BannerEnricher(urlHelper);
        }

        [HttpGet(Name = "GetAllBanners")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(ItemsLinkContainer<BannerViewModel>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ItemsLinkContainer<BannerViewModel> Get()
        {
            var banners = _bannerAppService.GetAll().ToList();
            banners.ForEach(x => x.AddRangeLink(_bannerEnricher.CreateLinks(Method.Get, x)));
            var result = new ItemsLinkContainer<BannerViewModel>()
            {
                Items = banners
            };
            result.AddRangeLink(_bannerEnricher.CreateLinks(Method.GetAll));
            return result;
        }

        [HttpGet("{id}", Name = "GetBannerById")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(BannerViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Get(int id)
        {
            var banner = _bannerAppService.GetById(id);
            if (banner != null)
            {
                banner.AddRangeLink(_bannerEnricher.CreateLinks(Method.Get, banner));
                return Ok(banner);
            }
            else
                return BadRequest();
        }

        [HttpPost(Name = "InsertBanner")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(BannerViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public BannerViewModel Post([FromBody]BannerViewModel banner)
        {
            banner = _bannerAppService.Add(banner);
            banner.AddRangeLink(_bannerEnricher.CreateLinks(Method.Post, banner));
            return banner;
        }

        [HttpPut("{id}", Name = "UpdateBanner")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(BannerViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Put(int id, [FromBody]BannerViewModel banner)
        {
            if (_bannerAppService.GetById(banner.Id).Id != 0)
            {
                banner = _bannerAppService.Update(banner);
                banner.AddRangeLink(_bannerEnricher.CreateLinks(Method.Put, banner));
                return Ok(banner);
            }
            else
                return BadRequest();
        }

        [HttpDelete("{id}", Name = "DeleteBanner")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
            if (_bannerAppService.GetById(id).Id != 0)
            {
                _bannerAppService.Delete(id);
                return NoContent();
            }
            else
                return BadRequest();
        }
    }
}
