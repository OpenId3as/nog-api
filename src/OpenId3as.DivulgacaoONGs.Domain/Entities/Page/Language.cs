using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Entities.Page
{
    public class Language : MongoEntity
    {
        public string Lang { get; set; }
        public DataPage Data { get; set; }
        public List<FooterLabels> Footer { get; set; }
        public List<HeaderLabels> Header { get; set; }
        public List<ValidationLabels> Validation { get; set; }

        public class HeaderLabels
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }

        public class FooterLabels
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }

        public class ValidationLabels
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }
    }
}
