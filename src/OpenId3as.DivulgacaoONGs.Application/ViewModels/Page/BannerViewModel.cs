using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.ViewModels.Page
{
    public class BannerViewModel : LinkContainer
    {
        public long Id { get; set; }
        public string Institution { get; set; }
        public List<DataPageViewModel> Data { get; set; }
    }
}
