using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.ViewModels.Page
{
    public class MenuViewModel : LinkContainer
    {
        public long Id { get; set; }
        public string Institution { get; set; }
        public StyleViewModel Style { get; set; }
        public List<MenuItemViewModel> MenuItems { get; set; }
    }
}
