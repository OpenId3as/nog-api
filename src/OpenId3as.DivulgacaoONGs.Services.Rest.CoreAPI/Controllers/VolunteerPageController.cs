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
    public class VolunteerPageController : ControllerBase
    {
        private readonly IVolunteerAppService _volunteerPageAppService;
        private readonly IDistributedCache _cache;
        private readonly VolunteerPageEnricher _volunteerPageEnricher;

        public VolunteerPageController(
            IDistributedCache cache,
            IVolunteerAppService volunteerPageAppService,
            IUrlHelper urlHelper)
        {
            _cache = cache;
            _volunteerPageAppService = volunteerPageAppService;
            _volunteerPageEnricher = new VolunteerPageEnricher(urlHelper);
        }

        [HttpGet(Name = "GetAllVolunteersPages")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(ItemsLinkContainer<VolunteerViewModel>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ItemsLinkContainer<VolunteerViewModel> Get()
        {
            var volunteers = _volunteerPageAppService.GetAll().ToList();
            volunteers.ForEach(x => x.AddRangeLink(_volunteerPageEnricher.CreateLinks(Method.Get, x)));
            var result = new ItemsLinkContainer<VolunteerViewModel>()
            {
                Items = volunteers
            };
            result.AddRangeLink(_volunteerPageEnricher.CreateLinks(Method.GetAll));
            return result;
        }

        [HttpGet("{id}", Name = "GetVolunteerPageById")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(VolunteerViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Get(long id)
        {
            var volunteer = _volunteerPageAppService.GetById(id);
            if (volunteer != null)
            {
                volunteer.AddRangeLink(_volunteerPageEnricher.CreateLinks(Method.Get, volunteer));
                return Ok(volunteer);
            }
            else
                return BadRequest();
        }

        [HttpPost(Name = "InsertVolunteerPage")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(VolunteerViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public VolunteerViewModel Post([FromBody]VolunteerViewModel volunteer)
        {
            volunteer = _volunteerPageAppService.Add(volunteer);
            volunteer.AddRangeLink(_volunteerPageEnricher.CreateLinks(Method.Post, volunteer));
            return volunteer;
        }

        [HttpPut("{id}", Name = "UpdateVolunteerPage")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(VolunteerViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Put(long id, [FromBody]VolunteerViewModel volunteer)
        {
            if (_volunteerPageAppService.GetById(volunteer.Id).Id != 0)
            {
                volunteer = _volunteerPageAppService.Update(volunteer);
                volunteer.AddRangeLink(_volunteerPageEnricher.CreateLinks(Method.Put, volunteer));
                return Ok(volunteer);
            }
            else
                return BadRequest();
        }

        [HttpDelete("{id}", Name = "DeleteVolunteerPage")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Delete(long id)
        {
            if (_volunteerPageAppService.GetById(id).Id != 0)
            {
                _volunteerPageAppService.Delete(id);
                return NoContent();
            }
            else
                return BadRequest();
        }
    }
}
