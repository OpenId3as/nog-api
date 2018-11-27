using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using OpenId3as.DivulgacaoONGs.Application.Interfaces.Page;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using System;
using System.Collections.Generic;

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

        [HttpGet]
        public IEnumerable<ContactViewModel> Get()
        {
            return _contactAppService.GetAll();
        }

        [HttpGet("{id}")]
        public ContactViewModel Get(Int64 id)
        {
            return _contactAppService.GetById(id);
        }

        [HttpPost]
        public ContactViewModel Post([FromBody]ContactViewModel contact)
        {
            return _contactAppService.Add(contact);
        }

        [HttpPut("{id}")]
        public ContactViewModel Put(Int64 id, [FromBody]ContactViewModel contact)
        {
            return _contactAppService.Update(contact);
        }

        [HttpDelete("{id}")]
        public void Delete(Int64 id)
        {
            _contactAppService.Delete(id);
        }
    }
}
