using Microsoft.AspNetCore.Mvc;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.Enum;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.HyperMedia
{
    public class BannerEnricher
    {
        private readonly IUrlHelper _urlHelper;

        public BannerEnricher(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public IEnumerable<Link> CreateLinks(Method method, BannerViewModel content = null)
        {
            var linkContainer = new LinkContainer();
            if (_urlHelper != null)
            {
                var getAll = new Link() { Method = "GET", Rel = "get all banners", Href = _urlHelper.Link("GetAllBanners", new { }) };
                var insert = new Link() { Method = "POST", Rel = "insert banner", Href = _urlHelper.Link("InsertBanner", new { }) };

                var getById = new Link();
                var update = new Link();
                var delete = new Link();

                if (content != null)
                {
                    getById = new Link() { Method = "GET", Rel = "get banner by institution", Href = _urlHelper.Link("GetBannerByInstitution", new { }) };
                    update = new Link() { Method = "PUT", Rel = "update banner", Href = _urlHelper.Link("UpdateBanner", new { id = content.Id }) };
                    delete = new Link() { Method = "DELETE", Rel = "delete banner", Href = _urlHelper.Link("DeleteBanner", new { id = content.Id }) };
                }

                switch (method)
                {
                    case Method.GetAll:
                        linkContainer.AddLink(getAll);
                        linkContainer.AddLink(insert);
                        break;
                    case Method.Get:
                        linkContainer.AddLink(getById);
                        linkContainer.AddLink(update);
                        linkContainer.AddLink(delete);
                        break;
                    case Method.Post:
                        linkContainer.AddLink(insert);
                        linkContainer.AddLink(getById);
                        linkContainer.AddLink(update);
                        linkContainer.AddLink(delete);
                        break;
                    case Method.Put:
                        linkContainer.AddLink(update);
                        linkContainer.AddLink(getById);
                        linkContainer.AddLink(delete);
                        break;
                }
                linkContainer.Links[0].Rel = "self";
            }
            return linkContainer.Links;
        }
    }
}
