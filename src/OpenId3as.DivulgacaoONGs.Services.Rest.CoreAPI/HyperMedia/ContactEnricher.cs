using Microsoft.AspNetCore.Mvc;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.Enum;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.HyperMedia
{
    public class ContactEnricher
    {
        private readonly IUrlHelper _urlHelper;

        public ContactEnricher(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public IEnumerable<Link> CreateLinks(Method method, ContactViewModel contact = null)
        {
            var linkContainer = new LinkContainer();
            if (_urlHelper != null)
            {
                var getAll = new Link() { Method = "GET", Rel = "get all contacts", Href = _urlHelper.Link("GetAllContacts", new { }) };
                var insert = new Link() { Method = "POST", Rel = "insert contact", Href = _urlHelper.Link("InsertContact", new { }) };

                var getById = new Link();
                var update = new Link();
                var delete = new Link();

                if (contact != null)
                {
                    getById = new Link() { Method = "GET", Rel = "get contact by id", Href = _urlHelper.Link("GetContactById", new { id = contact.Id }) };
                    update = new Link() { Method = "PUT", Rel = "update contact", Href = _urlHelper.Link("UpdateContact", new { id = contact.Id }) };
                    delete = new Link() { Method = "DELETE", Rel = "delete contact", Href = _urlHelper.Link("DeleteContact", new { id = contact.Id }) };
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
                    case Method.Delete:
                        linkContainer.AddLink(delete);
                        linkContainer.AddLink(getById);
                        linkContainer.AddLink(update);
                        break;
                }
                linkContainer.Links[0].Rel = "self";
            }
            return linkContainer.Links;
        }
    }
}
