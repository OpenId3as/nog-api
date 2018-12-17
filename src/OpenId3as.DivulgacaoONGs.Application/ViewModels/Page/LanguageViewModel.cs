using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;

namespace OpenId3as.DivulgacaoONGs.Application.ViewModels.Page
{
    public class LanguageViewModel : LinkContainer
    {
        public long Id { get; set; }
        public string Lang { get; set; }
        public DataPageViewModel Data { get; set; }
    }
}
