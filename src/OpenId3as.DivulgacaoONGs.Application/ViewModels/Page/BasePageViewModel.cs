using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.ViewModels.Page
{
    public class BasePageViewModel : LinkContainer
    {
        public string Institution { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<LanguagePageViewModel> Language { get; set; }
    }
    public class LanguagePageViewModel
    {
        public string Lang { get; set; }
        public List<DataPageViewModel> Data { get; set; }
    }
}
