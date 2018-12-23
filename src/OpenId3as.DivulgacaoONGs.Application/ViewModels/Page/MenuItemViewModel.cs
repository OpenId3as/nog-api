using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.ViewModels.Page
{
    public class MenuItemViewModel
    {
        public string Lang { get; set; }
        public List<MenuItemDetailViewModel> MenuDetails { get; set; }
    }
}
