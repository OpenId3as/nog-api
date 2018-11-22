using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.ViewModels.Page
{
    public class BasePageViewModel
    {
        public string Institution { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<DataPageViewModel> Data { get; set; }
    }
}
