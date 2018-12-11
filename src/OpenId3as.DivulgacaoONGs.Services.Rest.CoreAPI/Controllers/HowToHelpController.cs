using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using OpenId3as.DivulgacaoONGs.Application.Interfaces.Page;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.Enum;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Controllers
{
    [ApiVersionNeutral]
    [Route("api/[controller]")]
    public class HowToHelpController : Controller
    {
        private readonly IHowToHelpAppService _howToHelpAppService;
        private readonly IDistributedCache _cache;
        public HowToHelpController(IDistributedCache cache, IHowToHelpAppService howToHelpAppService)
        {
            _cache = cache;
            _howToHelpAppService = howToHelpAppService;
        }

        [HttpGet(Name = "GetAllHowToHelp")]
        [ProducesResponseType(201, Type = typeof(ItemsLinkContainer<HowToHelpViewModel>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ItemsLinkContainer<HowToHelpViewModel> Get()
        {
            var obj = _howToHelpAppService.GetAll().ToList();
            obj.ForEach(x => x.AddRangeLink(CreateLinks(Method.Get, x)));
            var result = new ItemsLinkContainer<HowToHelpViewModel>()
            {
                Items = obj
            };
            result.AddRangeLink(CreateLinks(Method.GetAll));
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
                obj.AddRangeLink(CreateLinks(Method.Get, obj));
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
            howToHelp.AddRangeLink(CreateLinks(Method.Post, howToHelp));
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
                howToHelp.AddRangeLink(CreateLinks(Method.Put, howToHelp));
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

        private IEnumerable<Link> CreateLinks(Method method, HowToHelpViewModel obj = null)
        {
            var linkContainer = new LinkContainer();
            if (Url != null)
            {
                var getAll = new Link() { Method = "GET", Rel = "get all 'how to help's", Href = Url.Link("GetAllHowToHelp", new { }) };
                var insert = new Link() { Method = "POST", Rel = "insert 'how to help'", Href = Url.Link("InsertHowToHelp", new { }) };

                var getById = new Link();
                var update = new Link();
                var delete = new Link();

                if (obj != null)
                {
                    getById = new Link() { Method = "GET", Rel = "get 'how to help' by id", Href = Url.Link("GetHowToHelpById", new { id = obj.Id }) };
                    update = new Link() { Method = "PUT", Rel = "update 'how to help'", Href = Url.Link("UpdateHowToHelp", new { id = obj.Id }) };
                    delete = new Link() { Method = "DELETE", Rel = "delete 'how to help'", Href = Url.Link("DeleteHowToHelp", new { id = obj.Id }) };
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
