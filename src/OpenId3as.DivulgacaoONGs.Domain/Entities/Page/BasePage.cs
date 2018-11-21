using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Entities.Page
{
    public class BasePage : MongoEntity
    {
        public string Institution { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<DataPage> Data { get; set; }
    }
}
