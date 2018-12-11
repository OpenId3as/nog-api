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
    public class WhoAreWeController : Controller
    {
        private readonly IWhoAreWeAppService _whoAreWeAppService;
        private readonly IDistributedCache _cache;
        public WhoAreWeController(IDistributedCache cache, IWhoAreWeAppService whoAreWeAppService)
        {
            _cache = cache;
            _whoAreWeAppService = whoAreWeAppService;
        }

        [HttpGet(Name = "GetAllWhoAreWe")]
        [ProducesResponseType(201, Type = typeof(ItemsLinkContainer<WhoAreWeViewModel>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ItemsLinkContainer<WhoAreWeViewModel> Get()
        {
            var whoAreWe = _whoAreWeAppService.GetAll().ToList();
            whoAreWe.ForEach(x => x.AddRangeLink(CreateLinks(Method.Get, x)));
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
                whoAreWe.AddRangeLink(CreateLinks(Method.Get, whoAreWe));
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
            whoAreWe.AddRangeLink(CreateLinks(Method.Post, whoAreWe));
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
                whoAreWe.AddRangeLink(CreateLinks(Method.Put, whoAreWe));
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

        private IEnumerable<Link> CreateLinks(Method method, WhoAreWeViewModel obj = null)
        {
            var linkContainer = new LinkContainer();
            if (Url != null)
            {
                var getAll = new Link() { Method = "GET", Rel = "get all 'who are we'", Href = Url.Link("GetAllWhoAreWe", new { }) };
                var insert = new Link() { Method = "POST", Rel = "insert ''who are we''", Href = Url.Link("InsertWhoAreWe", new { }) };

                var getById = new Link();
                var update = new Link();
                var delete = new Link();

                if (obj != null)
                {
                    getById = new Link() { Method = "GET", Rel = "get ''who are we'' by id", Href = Url.Link("GetWhoAreWeById", new { id = obj.Id }) };
                    update = new Link() { Method = "PUT", Rel = "update ''who are we''", Href = Url.Link("UpdateWhoAreWe", new { id = obj.Id }) };
                    delete = new Link() { Method = "DELETE", Rel = "delete ''who are we''", Href = Url.Link("DeleteWhoAreWe", new { id = obj.Id }) };
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
