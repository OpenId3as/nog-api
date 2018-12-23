using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Entities.Page
{
    public class MenuItem
    {
        public string Lang { get; set; }
        public List<MenuItemDetail> MenuDetails { get; set; }
    }
}
