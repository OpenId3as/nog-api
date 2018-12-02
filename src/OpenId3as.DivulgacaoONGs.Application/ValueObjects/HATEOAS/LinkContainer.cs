using Newtonsoft.Json;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS
{
    public class LinkContainer
    {
        public LinkContainer()
        {
            Links = new List<Link>();
        }

        [JsonProperty(PropertyName = "_links")]
        public List<Link> Links { get; set; }

        public void AddLink(string method, string rel, string href)
        {
            Links.Add(new Link
            {
                Method = method,
                Rel = rel,
                Href = href
            });
        }

        public void AddLink(Link link)
        {
            Links.Add(link);
        }

        public void AddRangeLink(IEnumerable<Link> links)
        {
            if (links != null)
                Links.AddRange(links);
        }
    }
}
