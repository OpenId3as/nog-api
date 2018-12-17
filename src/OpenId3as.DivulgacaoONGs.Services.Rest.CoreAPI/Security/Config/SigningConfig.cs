using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Security.Config
{
    public class SigningConfig
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfig()
        {
            using (var provider = new RSACryptoServiceProvider())
                Key = new RsaSecurityKey(provider);

            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}