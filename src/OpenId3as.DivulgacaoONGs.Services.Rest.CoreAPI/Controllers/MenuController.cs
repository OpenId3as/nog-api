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
    public class MenuController : BaseController
    {
        private readonly IMenuAppService _menuAppService;
        private readonly IDistributedCache _cache;
        private readonly MenuEnricher _menuEnricher;

        public MenuController(IDistributedCache cache, IMenuAppService menuAppService, IUrlHelper urlHelper)
        {
            _cache = cache;
            _menuAppService = menuAppService;
            _menuEnricher = new MenuEnricher(urlHelper);
        }

        [HttpGet(Name = "GetAllMenus")]
        [ProducesResponseType(201, Type = typeof(ItemsLinkContainer<MenuViewModel>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ItemsLinkContainer<MenuViewModel> Get()
        {
            var menus = _menuAppService.GetAll().ToList();
            menus.ForEach(x => x.AddRangeLink(_menuEnricher.CreateLinks(Method.Get, x)));
            var result = new ItemsLinkContainer<MenuViewModel>()
            {
                Items = menus
            };
            result.AddRangeLink(_menuEnricher.CreateLinks(Method.GetAll));
            return result;
        }

        [HttpGet("{id}", Name = "GetMenuById")]
        [ProducesResponseType(201, Type = typeof(MenuViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Get(int id)
        {
            var menu = _menuAppService.GetById(id);
            if (menu != null)
            {
                menu.AddRangeLink(_menuEnricher.CreateLinks(Method.Get, menu));
                return Ok(menu);
            }
            else
                return BadRequest();
        }

        [HttpPost(Name = "InsertMenu")]
        [ProducesResponseType(201, Type = typeof(MenuViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public MenuViewModel Post([FromBody]MenuViewModel menu)
        {
            menu = _menuAppService.Add(menu);
            menu.AddRangeLink(_menuEnricher.CreateLinks(Method.Post, menu));
            return menu;
        }

        [HttpPut("{id}", Name = "UpdateMenu")]
        [ProducesResponseType(201, Type = typeof(MenuViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Put(int id, [FromBody]MenuViewModel menu)
        {
            if (_menuAppService.GetById(menu.Id).Id != 0)
            {
                menu = _menuAppService.Update(menu);
                menu.AddRangeLink(_menuEnricher.CreateLinks(Method.Put, menu));
                return Ok(menu);
            }
            else
                return BadRequest();
        }

        [HttpDelete("{id}", Name = "DeleteMenu")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
            if (_menuAppService.GetById(id).Id != 0)
            {
                _menuAppService.Delete(id);
                return NoContent();
            }
            else
                return BadRequest();
        }
    }
}
