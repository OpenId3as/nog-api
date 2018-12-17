using Microsoft.AspNetCore.Mvc;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.Enum;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.HyperMedia
{
    public class LogoEnricher
    {
        private readonly IUrlHelper _urlHelper;

        public LogoEnricher(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public IEnumerable<Link> CreateLinks(Method method, LogoViewModel content = null)
        {
            var linkContainer = new LinkContainer();
            if (_urlHelper != null)
            {
                var getAll = new Link() { Method = "GET", Rel = "get all logos", Href = _urlHelper.Link("GetAllLogos", new { }) };
                var insert = new Link() { Method = "POST", Rel = "insert logo", Href = _urlHelper.Link("InsertLogo", new { }) };

                var getById = new Link();
                var update = new Link();
                var delete = new Link();

                if (content != null)
                {
                    getById = new Link() { Method = "GET", Rel = "get logo by id", Href = _urlHelper.Link("GetLogoById", new { id = content.Id }) };
                    update = new Link() { Method = "PUT", Rel = "update logo", Href = _urlHelper.Link("UpdateLogo", new { id = content.Id }) };
                    delete = new Link() { Method = "DELETE", Rel = "delete logo", Href = _urlHelper.Link("DeleteLogo", new { id = content.Id }) };
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
