using System;

namespace OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Security
{
    public interface IToken
    {
        string CreateLoginToken(string login, DateTime createDate, DateTime expirationDate);
    }
}
