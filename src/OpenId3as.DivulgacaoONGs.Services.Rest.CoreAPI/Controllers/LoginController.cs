using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenId3as.DivulgacaoONGs.Application.Interfaces;
using OpenId3as.DivulgacaoONGs.Application.ViewModels;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Controllers
{
    [ApiVersionNeutral]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginAppService _loginAppService;

        public LoginController(ILoginAppService loginAppService)
        {
            _loginAppService = loginAppService;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(object))]
        [ProducesResponseType(400)]
        public object Post([FromBody] UserViewModel user)
        {
            if (user == null) return BadRequest();
            return _loginAppService.GetByLogin(user);
        }
    }
}
