using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Entities.Page
{
    public class Menu : MongoEntity
    {
        public string Institution { get; set; }
        public Style Style { get; set; }
        public List<MenuItem> MenuItems { get; set; }
    }
}
