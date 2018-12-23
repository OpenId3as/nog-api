using Microsoft.AspNetCore.Mvc;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.Enum;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;
using OpenId3as.DivulgacaoONGs.Application.ViewModels;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.HyperMedia
{
    public class UserEnricher
    {
        private readonly IUrlHelper _urlHelper;

        public UserEnricher(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public IEnumerable<Link> CreateLinks(Method method, UserViewModel content = null)
        {
            var linkContainer = new LinkContainer();
            if (_urlHelper != null)
            {
                var getAll = new Link() { Method = "GET", Rel = "get all users", Href = _urlHelper.Link("GetAllUsers", new { }) };
                var insert = new Link() { Method = "POST", Rel = "insert user", Href = _urlHelper.Link("InsertUser", new { }) };

                var getById = new Link();
                var update = new Link();
                var delete = new Link();

                if (content != null)
                {
                    getById = new Link() { Method = "GET", Rel = "get user by id", Href = _urlHelper.Link("GetUserById", new { id = content.Id }) };
                    update = new Link() { Method = "PUT", Rel = "update user", Href = _urlHelper.Link("UpdateUser", new { id = content.Id }) };
                    delete = new Link() { Method = "DELETE", Rel = "delete user", Href = _urlHelper.Link("DeleteUser", new { id = content.Id }) };
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
