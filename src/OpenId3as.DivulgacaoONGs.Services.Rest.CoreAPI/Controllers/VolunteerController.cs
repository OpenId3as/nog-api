using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using OpenId3as.DivulgacaoONGs.Application.Interfaces;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Volunteers;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Controllers
{
    [ApiVersionNeutral]
    [Route("api/[controller]")]
    public class VolunteerController : BaseController
    {
        private readonly IVolunteerAppService _volunteerAppService;
        private readonly IDistributedCache _cache;
        public VolunteerController(IDistributedCache cache, IVolunteerAppService volunteerAppService)
        {
            _cache = cache;
            _volunteerAppService = volunteerAppService;
        }

        [HttpGet]
        public IEnumerable<VolunteerViewModel> Get()
        {
            return _volunteerAppService.GetAll();
        }

        [HttpGet("{id}")]
        public VolunteerViewModel Get(Int64 id)
        {
            return _volunteerAppService.GetById(id);
        }

        [HttpPost]
        public VolunteerViewModel Post([FromBody]VolunteerViewModel volunteer)
        {
            return _volunteerAppService.Add(volunteer);
        }

        [HttpPut("{id}")]
        public VolunteerViewModel Put(Int64 id, [FromBody]VolunteerViewModel volunteer)
        {
            return _volunteerAppService.Update(volunteer);
        }

        [HttpDelete("{id}")]
        public void Delete(Int64 id)
        {
            _volunteerAppService.Delete(id);
        }
    }
}
