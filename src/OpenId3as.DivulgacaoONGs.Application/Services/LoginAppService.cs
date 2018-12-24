using OpenId3as.DivulgacaoONGs.Application.Interfaces;
using OpenId3as.DivulgacaoONGs.Application.ViewModels;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services;
using OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Security;
using OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Security.Config;
using System;

namespace OpenId3as.DivulgacaoONGs.Application.Services
{
    public class LoginAppService : ILoginAppService
    {
        private readonly IToken _token;
        private readonly IUserService _userService;
        private SigningConfig _signingConfig;
        private TokenConfig _tokenConfig;

        public LoginAppService(IToken token, IUserService userService, SigningConfig signingConfig, TokenConfig tokenConfig)
        {
            _token = token;
            _userService = userService;
            _signingConfig = signingConfig;
            _tokenConfig = tokenConfig;
        }

        public void Dispose()
        {

        }

        public object GetByLogin(UserViewModel userViewModel)
        {
            bool credentialsIsValid = false;
            if (userViewModel != null && !string.IsNullOrWhiteSpace(userViewModel.Login))
            {
                var baseUser = _userService.GetByLogin(userViewModel.Login);
                credentialsIsValid = (baseUser != null && userViewModel.Login == baseUser.Login && userViewModel.Password == baseUser.Password);
            }

            if (credentialsIsValid)
            {
                DateTime createDate = DateTime.Now;
                DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfig.Seconds);
                var token = _token.CreateLoginToken(userViewModel.Login, createDate, expirationDate);
                return SuccessObject(createDate, expirationDate, token);
            }
            else
                return ExceptionObject();
        }

        private object ExceptionObject()
        {
            return new
            {
                autenticated = false,
                message = "Failed to autheticate"
            };
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token)
        {
            return new
            {
                autenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                message = "OK"
            };
        }
    }
}
