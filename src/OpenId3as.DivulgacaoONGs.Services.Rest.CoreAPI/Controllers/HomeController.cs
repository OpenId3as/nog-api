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
    public class HomeController : Controller
    {
        private readonly IHomeAppService _homeAppService;
        private readonly IDistributedCache _cache;
        public HomeController(IDistributedCache cache, IHomeAppService homeAppService)
        {
            _cache = cache;
            _homeAppService = homeAppService;
        }

        [HttpGet(Name = "GetAllHomes")]
        [ProducesResponseType(201, Type = typeof(ItemsLinkContainer<HomeViewModel>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ItemsLinkContainer<HomeViewModel> Get()
        {
            var obj = _homeAppService.GetAll().ToList();
            obj.ForEach(x => x.AddRangeLink(CreateLinks(Method.Get, x)));
            var result = new ItemsLinkContainer<HomeViewModel>()
            {
                Items = obj
            };
            result.AddRangeLink(CreateLinks(Method.GetAll));
            return result;
        }

        [HttpGet("{id}", Name = "GetHomeById")]
        [ProducesResponseType(201, Type = typeof(HomeViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Get(long id)
        {
            var home = _homeAppService.GetById(id);
            if (home != null)
            {
                home.AddRangeLink(CreateLinks(Method.Get, home));
                return Ok(home);
            }
            else
                return BadRequest();
        }

        [HttpPost(Name = "InsertHome")]
        [ProducesResponseType(201, Type = typeof(HomeViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public HomeViewModel Post([FromBody]HomeViewModel home)
        {
            home = _homeAppService.Add(home);
            home.AddRangeLink(CreateLinks(Method.Post, home));
            return home;
        }

        [HttpPut("{id}", Name = "UpdateHome")]
        [ProducesResponseType(201, Type = typeof(HomeViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Put(long id, [FromBody]HomeViewModel home)
        {
            if (_homeAppService.GetById(home.Id).Id != 0)
            {
                home = _homeAppService.Update(home);
                home.AddRangeLink(CreateLinks(Method.Put, home));
                return Ok(home);
            }
            else
                return BadRequest();
        }

        [HttpDelete("{id}", Name = "DeleteHome")]
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

        private IEnumerable<Link> CreateLinks(Method method, HomeViewModel home = null)
        {
            var linkContainer = new LinkContainer();
            if (Url != null)
            {
                var getAll = new Link() { Method = "GET", Rel = "get all homes", Href = Url.Link("GetAllHomes", new { }) };
                var insert = new Link() { Method = "POST", Rel = "insert home", Href = Url.Link("InsertHome", new { }) };

                var getById = new Link();
                var update = new Link();
                var delete = new Link();

                if (home != null)
                {
                    getById = new Link() { Method = "GET", Rel = "get home by id", Href = Url.Link("GetHomeById", new { id = home.Id }) };
                    update = new Link() { Method = "PUT", Rel = "update home", Href = Url.Link("UpdateHome", new { id = home.Id }) };
                    delete = new Link() { Method = "DELETE", Rel = "delete home", Href = Url.Link("DeleteHome", new { id = home.Id }) };
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
