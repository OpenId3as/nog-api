using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Security.Config
{
    public class SigningConfig
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfig()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
                Key = new RsaSecurityKey(provider);

            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}
