namespace OpenId3as.DivulgacaoONGs.Domain.Entities.Page
{
    public class Language : MongoEntity
    {
        public string Lang { get; set; }
        public DataLabels Data { get; set; }
    }

    public class DataLabels
    {
        public FooterLabels Footer { get; set; }
        public HeaderLabels Header { get; set; }
        public ValidationLabels Validation { get; set; }
    }

    public class FooterLabels
    {
        public string MapOfSite { get; set; }
        public string ReservedRights { get; set; }
    }

    public class HeaderLabels
    {
        public string FormatDate { get; set; }
        public string Hello { get; set; }
        public string ProvidedFor { get; set; }
        public string Search { get; set; }
        public string Title { get; set; }
        public string Welcome { get; set; }
    }

    public class ValidationLabels
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
