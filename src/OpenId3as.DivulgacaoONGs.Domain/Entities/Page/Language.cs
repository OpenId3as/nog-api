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
        public string ProvidedFor { get; set; }
        public string ReservedRights { get; set; }
    }

    public class HeaderLabels
    {
        public string Hello { get; set; }
        public string Title { get; set; }
        public string Welcome { get; set; }
        public string LoginButton { get; set; }
    }

    public class ValidationLabels
    {
    }
}
