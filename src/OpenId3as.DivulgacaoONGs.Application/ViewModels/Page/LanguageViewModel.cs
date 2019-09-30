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
        public string MapOfSite { get; set; }
        public string ReservedRights { get; set; }
    }

    public class HeaderLabelsViewModel
    {
        public string FormatDate { get; set; }
        public string Hello { get; set; }
        public string ProvidedFor { get; set; }
        public string Search { get; set; }
        public string Title { get; set; }
        public string Welcome { get; set; }
    }

    public class ValidationLabelsViewModel
    {
        public string EmailDoesntMatch { get; set; }
        public string ImNotRobot { get; set; }
        public string IncompleteField { get; set; }
        public string InvalidField { get; set; }
        public string OnlyNumbers { get; set; }
        public string OnlySixCharacters { get; set; }
        public string OnlySixNumbers { get; set; }
        public string PasswordDoesntMatch { get; set; }
        public string RequiredField { get; set; }
    }
}
