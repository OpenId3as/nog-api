using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;

namespace OpenId3as.DivulgacaoONGs.Application.ViewModels.Page
{
    public class LanguageViewModel : LinkContainer
    {
        public long Id { get; set; }
        public string Lang { get; set; }
        public DataLabelsViewModel Data { get; set; }
    }

    public class DataLabelsViewModel
    {
        public FooterLabelsViewModel Footer { get; set; }
        public HeaderLabelsViewModel Header { get; set; }
        public ValidationLabelsViewModel Validation { get; set; }
    }

    public class FooterLabelsViewModel
    {
        public string ProvidedFor { get; set; }
        public string ReservedRights { get; set; }
    }

    public class HeaderLabelsViewModel
    {
        public string Hello { get; set; }
        public string Title { get; set; }
        public string Welcome { get; set; }
        public string LoginButton { get; set; }
    }

    public class ValidationLabelsViewModel
    {
    }
}
