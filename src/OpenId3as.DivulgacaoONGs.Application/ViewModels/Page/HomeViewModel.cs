using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.ViewModels.Page
{
    public class HomeViewModel : BasePageViewModel
    {
        public long Id { get; set; }
        public List<DataPageViewModel> Banner { get; set; }
    }
}
