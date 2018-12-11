using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using OpenId3as.DivulgacaoONGs.Application.Interfaces.Page;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.Enum;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using System.Collections.Generic;
using System.Linq;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Controllers
{
    [ApiVersionNeutral]
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private readonly IContactAppService _contactAppService;
        private readonly IDistributedCache _cache;
        public ContactController(IDistributedCache cache, IContactAppService contactAppService)
        {
            _cache = cache;
            _contactAppService = contactAppService;
        }

        [HttpGet(Name = "GetAllContacts")]
        [ProducesResponseType(201, Type = typeof(ItemsLinkContainer<ContactViewModel>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ItemsLinkContainer<ContactViewModel> Get()
        {
            var contact = _contactAppService.GetAll().ToList();
            contact.ForEach(x => x.AddRangeLink(CreateLinks(Method.Get, x)));
            var result = new ItemsLinkContainer<ContactViewModel>()
            {
                Items = contact
            };
            result.AddRangeLink(CreateLinks(Method.GetAll));
            return result;
        }

        [HttpGet("{id}", Name = "GetContactById")]
        [ProducesResponseType(201, Type = typeof(ContactViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Get(long id)
        {
            var contact = _contactAppService.GetById(id);
            if (contact != null)
            {
                contact.AddRangeLink(CreateLinks(Method.Get, contact));
                return Ok(contact);
            }
            else
                return BadRequest();
        }

        [HttpPost(Name = "InsertContact")]
        [ProducesResponseType(201, Type = typeof(ContactViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ContactViewModel Post([FromBody]ContactViewModel contact)
        {
            contact = _contactAppService.Add(contact);
            contact.AddRangeLink(CreateLinks(Method.Post, contact));
            return contact;
        }

        [HttpPut("{id}", Name = "UpdateContact")]
        [ProducesResponseType(201, Type = typeof(ContactViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Put(long id, [FromBody]ContactViewModel contact)
        {
            if (_contactAppService.GetById(id).Id != 0)
            {
                contact = _contactAppService.Update(contact);
                contact.AddRangeLink(CreateLinks(Method.Put, contact));
                return Ok(contact);
            }
            else
                return BadRequest();
        }

        [HttpDelete("{id}", Name = "DeleteContact")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Delete(long id)
        {
            if (_contactAppService.GetById(id).Id != 0)
            {
                _contactAppService.Delete(id);
                return NoContent();
            }
            else
                return BadRequest();
        }

        private IEnumerable<Link> CreateLinks(Method method, ContactViewModel contact = null)
        {
            var linkContainer = new LinkContainer();
            if (Url != null)
            {
                var getAll = new Link() { Method = "GET", Rel = "get all contacts", Href = Url.Link("GetAllContacts", new { }) };
                var insert = new Link() { Method = "POST", Rel = "insert contact", Href = Url.Link("InsertContact", new { }) };

                var getById = new Link();
                var update = new Link();
                var delete = new Link();

                if (contact != null)
                {
                    getById = new Link() { Method = "GET", Rel = "get contact by id", Href = Url.Link("GetContactById", new { id = contact.Id }) };
                    update = new Link() { Method = "PUT", Rel = "update contact", Href = Url.Link("UpdateContact", new { id = contact.Id }) };
                    delete = new Link() { Method = "DELETE", Rel = "delete contact", Href = Url.Link("DeleteContact", new { id = contact.Id }) };
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
