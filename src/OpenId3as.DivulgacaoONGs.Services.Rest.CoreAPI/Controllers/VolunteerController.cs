using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using OpenId3as.DivulgacaoONGs.Application.Interfaces;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.Enum;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Volunteers;
using OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Security;
using OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.HyperMedia;
using System.Linq;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Controllers
{
    [ApiVersionNeutral]
    [Route("api/[controller]")]
    [Authorize]
    public class VolunteerController : BaseController
    {
        private readonly IVolunteerAppService _volunteerAppService;
        private readonly IDistributedCache _cache;
        private readonly VolunteerEnricher _volunteerEnricher;

        public VolunteerController(IDistributedCache cache, IVolunteerAppService volunteerAppService, IUrlHelper urlHelper)
        {
            _cache = cache;
            _volunteerAppService = volunteerAppService;
            _volunteerEnricher = new VolunteerEnricher(urlHelper);
        }

        [HttpGet(Name = "GetAllVolunteers")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(ItemsLinkContainer<VolunteerViewModel>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ItemsLinkContainer<VolunteerViewModel> Get()
        {
            var volunteer = _volunteerAppService.GetAll().ToList();
            volunteer.ForEach(x => x.AddRangeLink(_volunteerEnricher.CreateLinks(Method.Get, x)));
            var result = new ItemsLinkContainer<VolunteerViewModel>()
            {
                Items = volunteer
            };
            result.AddRangeLink(_volunteerEnricher.CreateLinks(Method.GetAll));
            return result;
        }

        [HttpGet("{id}", Name = "GetVolunteerById")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(VolunteerViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Get(long id)
        {
            var volunteer = _volunteerAppService.GetById(id);
            if (volunteer != null)
            {
                volunteer.AddRangeLink(_volunteerEnricher.CreateLinks(Method.Get, volunteer));
                return Ok(volunteer);
            }
            else
                return BadRequest();
        }

        [HttpPost(Name = "InsertVolunteer")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(VolunteerViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public VolunteerViewModel Post([FromBody]VolunteerViewModel volunteer)
        {
            volunteer = _volunteerAppService.Add(volunteer);
            volunteer.AddRangeLink(_volunteerEnricher.CreateLinks(Method.Post, volunteer));
            return volunteer;
        }

        [HttpPut("{id}", Name = "UpdateVolunteer")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(VolunteerViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Put(long id, [FromBody]VolunteerViewModel volunteer)
        {
            if (_volunteerAppService.GetById(volunteer.Id).Id != 0)
            {
                volunteer = _volunteerAppService.Update(volunteer);
                volunteer.AddRangeLink(_volunteerEnricher.CreateLinks(Method.Put, volunteer));
                return Ok(volunteer);
            }
            else
                return BadRequest();
        }

        [HttpDelete("{id}", Name = "DeleteVolunteer")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Delete(long id)
        {
            if (_volunteerAppService.GetById(id).Id != 0)
            {
                _volunteerAppService.Delete(id);
                return NoContent();
            }
            else
                return BadRequest();
        }
    }
}
