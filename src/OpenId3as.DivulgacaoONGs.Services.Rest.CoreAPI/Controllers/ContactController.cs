using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using OpenId3as.DivulgacaoONGs.Application.Interfaces.Page;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.Enum;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Security;
using OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.HyperMedia;
using System.Linq;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Controllers
{
    [ApiVersionNeutral]
    [Route("api/[controller]")]
    [Authorize]
    public class ContactController : Controller
    {
        private readonly IContactAppService _contactAppService;
        private readonly IDistributedCache _cache;
        private readonly ContactEnricher _contactEnricher;

        public ContactController(IDistributedCache cache, IContactAppService contactAppService, IUrlHelper urlHelper)
        {
            _cache = cache;
            _contactAppService = contactAppService;
            _contactEnricher = new ContactEnricher(urlHelper);
        }

        [HttpGet(Name = "GetAllContacts")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(ItemsLinkContainer<ContactViewModel>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ItemsLinkContainer<ContactViewModel> Get()
        {
            var contact = _contactAppService.GetAll().ToList();
            contact.ForEach(x => x.AddRangeLink(_contactEnricher.CreateLinks(Method.Get, x)));
            var result = new ItemsLinkContainer<ContactViewModel>()
            {
                Items = contact
            };
            result.AddRangeLink(_contactEnricher.CreateLinks(Method.GetAll));
            return result;
        }

        [HttpGet("{id}", Name = "GetContactById")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(ContactViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Get(long id)
        {
            var contact = _contactAppService.GetById(id);
            if (contact != null)
            {
                contact.AddRangeLink(_contactEnricher.CreateLinks(Method.Get, contact));
                return Ok(contact);
            }
            else
                return BadRequest();
        }

        [HttpPost(Name = "InsertContact")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(ContactViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ContactViewModel Post([FromBody]ContactViewModel contact)
        {
            contact = _contactAppService.Add(contact);
            contact.AddRangeLink(_contactEnricher.CreateLinks(Method.Post, contact));
            return contact;
        }

        [HttpPut("{id}", Name = "UpdateContact")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(ContactViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Put(long id, [FromBody]ContactViewModel contact)
        {
            if (_contactAppService.GetById(id).Id != 0)
            {
                contact = _contactAppService.Update(contact);
                contact.AddRangeLink(_contactEnricher.CreateLinks(Method.Put, contact));
                return Ok(contact);
            }
            else
                return BadRequest();
        }

        [HttpDelete("{id}", Name = "DeleteContact")]
        [Authorize(AuthorizationNameOptions.Bearer)]
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
    }
}
