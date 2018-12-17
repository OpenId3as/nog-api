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
    public class WhoAreWeController : Controller
    {
        private readonly IWhoAreWeAppService _whoAreWeAppService;
        private readonly IDistributedCache _cache;
        private readonly WhoAreWeEnricher _whoAreWeEnricher;

        public WhoAreWeController(IDistributedCache cache, IWhoAreWeAppService whoAreWeAppService, IUrlHelper urlHelper)
        {
            _cache = cache;
            _whoAreWeAppService = whoAreWeAppService;
            _whoAreWeEnricher = new WhoAreWeEnricher(urlHelper);
        }

        [HttpGet(Name = "GetAllWhoAreWe")]
        [ProducesResponseType(201, Type = typeof(ItemsLinkContainer<WhoAreWeViewModel>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ItemsLinkContainer<WhoAreWeViewModel> Get()
        {
            var whoAreWe = _whoAreWeAppService.GetAll().ToList();
            whoAreWe.ForEach(x => x.AddRangeLink(_whoAreWeEnricher.CreateLinks(Method.Get, x)));
            var result = new ItemsLinkContainer<WhoAreWeViewModel>()
            {
                Items = whoAreWe
            };
            result.AddRangeLink(CreateLinks(Method.GetAll));
            return result;
        }

        [HttpGet("{id}", Name = "GetWhoAreWeById")]
        [ProducesResponseType(201, Type = typeof(WhoAreWeViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Get(long id)
        {
            var whoAreWe = _whoAreWeAppService.GetById(id);
            if (whoAreWe != null)
            {
                whoAreWe.AddRangeLink(_whoAreWeEnricher.CreateLinks(Method.Get, whoAreWe));
                return Ok(whoAreWe);
            }
            else
                return BadRequest();
        }

        [HttpPost(Name = "InsertWhoAreWe")]
        [ProducesResponseType(201, Type = typeof(WhoAreWeViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public WhoAreWeViewModel Post([FromBody]WhoAreWeViewModel whoAreWe)
        {
            whoAreWe = _whoAreWeAppService.Add(whoAreWe);
            whoAreWe.AddRangeLink(_whoAreWeEnricher.CreateLinks(Method.Post, whoAreWe));
            return whoAreWe;
        }

        [HttpPut("{id}", Name = "UpdateWhoAreWe")]
        [ProducesResponseType(201, Type = typeof(WhoAreWeViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Put(long id, [FromBody]WhoAreWeViewModel whoAreWe)
        {
            if (_whoAreWeAppService.GetById(whoAreWe.Id).Id != 0)
            {
                whoAreWe = _whoAreWeAppService.Update(whoAreWe);
                whoAreWe.AddRangeLink(_whoAreWeEnricher.CreateLinks(Method.Put, whoAreWe));
                return Ok(whoAreWe);
            }
            else
                return BadRequest();
        }

        [HttpDelete("{id}", Name = "DeleteWhoAreWe")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Delete(long id)
        {
            if (_whoAreWeAppService.GetById(id).Id != 0)
            {
                _whoAreWeAppService.Delete(id);
                return NoContent();
            }
            else
                return BadRequest();
        }
    }
}
