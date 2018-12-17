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
    public class HowToHelpController : Controller
    {
        private readonly IHowToHelpAppService _howToHelpAppService;
        private readonly IDistributedCache _cache;
        private readonly HowToHelpEnricher _howToHelpEnricher;

        public HowToHelpController(IDistributedCache cache, IHowToHelpAppService howToHelpAppService, IUrlHelper urlHelper)
        {
            _cache = cache;
            _howToHelpAppService = howToHelpAppService;
            _howToHelpEnricher = new HowToHelpEnricher(urlHelper);

        }

        [HttpGet(Name = "GetAllHowToHelp")]
        [ProducesResponseType(201, Type = typeof(ItemsLinkContainer<HowToHelpViewModel>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ItemsLinkContainer<HowToHelpViewModel> Get()
        {
            var obj = _howToHelpAppService.GetAll().ToList();
            obj.ForEach(x => x.AddRangeLink(_howToHelpEnricher.CreateLinks(Method.Get, x)));
            var result = new ItemsLinkContainer<HowToHelpViewModel>()
            {
                Items = obj
            };
            result.AddRangeLink(_howToHelpEnricher.CreateLinks(Method.GetAll));
            return result;
        }

        [HttpGet("{id}", Name = "GetHowToHelpById")]
        [ProducesResponseType(201, Type = typeof(HowToHelpViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Get(long id)
        {
            var obj = _howToHelpAppService.GetById(id);
            if (obj != null)
            {
                obj.AddRangeLink(_howToHelpEnricher.CreateLinks(Method.Get, obj));
                return Ok(obj);
            }
            else
                return BadRequest();
        }

        [HttpPost(Name = "InsertHowToHelp")]
        [ProducesResponseType(201, Type = typeof(HowToHelpViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public HowToHelpViewModel Post([FromBody]HowToHelpViewModel howToHelp)
        {
            howToHelp = _howToHelpAppService.Add(howToHelp);
            howToHelp.AddRangeLink(_howToHelpEnricher.CreateLinks(Method.Post, howToHelp));
            return howToHelp;
        }

        [HttpPut("{id}", Name = "UpdateHowToHelp")]
        [ProducesResponseType(201, Type = typeof(HowToHelpViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Put(long id, [FromBody]HowToHelpViewModel howToHelp)
        {
            if (_howToHelpAppService.GetById(howToHelp.Id).Id != 0)
            {
                howToHelp = _howToHelpAppService.Update(howToHelp);
                howToHelp.AddRangeLink(_howToHelpEnricher.CreateLinks(Method.Put, howToHelp));
                return Ok(howToHelp);
            }
            else
                return BadRequest();
        }

        [HttpDelete("{id}", Name = "DeleteHowToHelp")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Delete(long id)
        {
            if (_howToHelpAppService.GetById(id).Id != 0)
            {
                _howToHelpAppService.Delete(id);
                return NoContent();
            }
            else
                return BadRequest();
        }
    }
}
