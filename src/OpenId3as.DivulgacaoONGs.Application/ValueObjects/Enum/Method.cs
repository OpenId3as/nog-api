using System.ComponentModel;

namespace OpenId3as.DivulgacaoONGs.Application.ValueObjects.Enum
{
    public enum Method
    {
        [Description("GET")]
        GetAll = 1,
        [Description("GET")]
        Get = 2,
        [Description("POST")]
        Post = 3,
        [Description("PUT")]
        Put = 4,
        [Description("DELETE")]
        Delete = 5,
    }
}
