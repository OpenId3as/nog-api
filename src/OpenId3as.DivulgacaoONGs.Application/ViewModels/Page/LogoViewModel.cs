using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;

namespace OpenId3as.DivulgacaoONGs.Application.ViewModels.Page
{
    public class LogoViewModel : LinkContainer
    {
        public long Id { get; set; }
        public string Institution { get; set; }
        public DataPageViewModel Data { get; set; }
    }
}
