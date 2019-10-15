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
        private readonly IUserAppService _UserAppService;
        private readonly IDistributedCache _cache;
        private readonly UserEnricher _UserEnricher;

        public UserController(IDistributedCache cache, IUserAppService UserAppService, IUrlHelper urlHelper)
        {
            _cache = cache;
            _UserAppService = UserAppService;
            _UserEnricher = new UserEnricher(urlHelper);
        }

        [HttpGet(Name = "GetAllUsers")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(ItemsLinkContainer<UserViewModel>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ItemsLinkContainer<UserViewModel> Get()
        {
            var User = _UserAppService.GetAll().ToList();
            User.ForEach(x => x.AddRangeLink(_UserEnricher.CreateLinks(Method.Get, x)));
            var result = new ItemsLinkContainer<UserViewModel>()
            {
                Items = User
            };
            result.AddRangeLink(_UserEnricher.CreateLinks(Method.GetAll));
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
            var User = _UserAppService.GetById(id);
            if (User != null)
            {
                User.AddRangeLink(_UserEnricher.CreateLinks(Method.Get, User));
                return Ok(User);
            }
            else
                return BadRequest();
        }

        [HttpPost(Name = "InsertUser")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(UserViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public UserViewModel Post([FromBody]UserViewModel User)
        {
            User = _UserAppService.Add(User);
            User.AddRangeLink(_UserEnricher.CreateLinks(Method.Post, User));
            return User;
        }

        [HttpPut("{id}", Name = "UpdateUser")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(UserViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Put(long id, [FromBody]UserViewModel User)
        {
            if (_UserAppService.GetById(User.Id).Id != 0)
            {
                User = _UserAppService.Update(User);
                User.AddRangeLink(_UserEnricher.CreateLinks(Method.Put, User));
                return Ok(User);
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
            if (_UserAppService.GetById(id).Id != 0)
            {
                _UserAppService.Delete(id);
                return NoContent();
            }
            else
                return BadRequest();
        }
    }
}
