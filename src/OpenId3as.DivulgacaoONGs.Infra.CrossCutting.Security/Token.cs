using Microsoft.IdentityModel.Tokens;
using OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Security.Config;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Security
{
    public class Token : IToken
    {
        private SigningConfig _signingConfig;
        private TokenConfig _tokenConfig;

        public Token(SigningConfig signingConfig, TokenConfig tokenConfig)
        {
            _signingConfig = signingConfig;
            _tokenConfig = tokenConfig;
        }

        public string CreateLoginToken(string login, DateTime createDate, DateTime expirationDate)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(login, "Login"),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                            new Claim(JwtRegisteredClaimNames.UniqueName, login)
                        }
                    );

            var handler = new JwtSecurityTokenHandler();
            string token = CreateToken(identity, createDate, expirationDate, handler);

            return token;
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfig.Issuer,
                Audience = _tokenConfig.Audience,
                SigningCredentials = _signingConfig.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }
    }
}
