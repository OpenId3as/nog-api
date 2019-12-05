using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using OpenId3as.DivulgacaoONGs.Application.Interfaces;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.Enum;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;
using OpenId3as.DivulgacaoONGs.Application.ViewModels;
using OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Security;
using OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.HyperMedia;
using System.Linq;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Controllers
{
    [ApiVersionNeutral]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly IDistributedCache _cache;
        private readonly UserEnricher _userEnricher;

        public UserController(IDistributedCache cache, IUserAppService userAppService, IUrlHelper urlHelper)
        {
            _cache = cache;
            _userAppService = userAppService;
            _userEnricher = new UserEnricher(urlHelper);
        }

        [HttpGet(Name = "GetAllUsers")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(ItemsLinkContainer<UserViewModel>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ItemsLinkContainer<UserViewModel> Get()
        {
            var user = _userAppService.GetAll().ToList();
            user.ForEach(x => x.AddRangeLink(_userEnricher.CreateLinks(Method.Get, x)));
            var result = new ItemsLinkContainer<UserViewModel>()
            {
                Items = user
            };
            result.AddRangeLink(_userEnricher.CreateLinks(Method.GetAll));
            return result;
        }

        [HttpGet("{id}", Name = "GetUserById")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(UserViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Get(long id)
        {
            var user = _userAppService.GetById(id);
            if (user != null)
            {
                user.AddRangeLink(_userEnricher.CreateLinks(Method.Get, user));
                return Ok(user);
            }
            else
                return BadRequest();
        }

        [HttpPost(Name = "InsertUser")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(UserViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public UserViewModel Post([FromBody]UserViewModel user)
        {
            user = _userAppService.Add(user);
            user.AddRangeLink(_userEnricher.CreateLinks(Method.Post, user));
            return user;
        }

        [HttpPut("{id}", Name = "UpdateUser")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(UserViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Put(long id, [FromBody]UserViewModel user)
        {
            if (_userAppService.GetById(user.Id).Id != 0)
            {
                user = _userAppService.Update(user);
                user.AddRangeLink(_userEnricher.CreateLinks(Method.Put, user));
                return Ok(user);
            }
            else
                return BadRequest();
        }

        [HttpDelete("{id}", Name = "DeleteUser")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Delete(long id)
        {
            if (_userAppService.GetById(id).Id != 0)
            {
                _userAppService.Delete(id);
                return NoContent();
            }
            else
                return BadRequest();
        }
    }
}
