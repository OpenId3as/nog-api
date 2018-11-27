using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS
{
    public class ItemsLinkContainer<T> : LinkContainer
    {
        public ItemsLinkContainer()
        {
        }

        public List<T> Items { get; set; }
    }
}
