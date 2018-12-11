using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using OpenId3as.DivulgacaoONGs.Application.Interfaces;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.Enum;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Volunteers;
using System.Collections.Generic;
using System.Linq;

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

        [HttpGet(Name = "GetAllVolunteers")]
        [ProducesResponseType(201, Type = typeof(ItemsLinkContainer<VolunteerViewModel>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ItemsLinkContainer<VolunteerViewModel> Get()
        {
            var volunteer = _volunteerAppService.GetAll().ToList();
            volunteer.ForEach(x => x.AddRangeLink(CreateLinks(Method.Get, x)));
            var result = new ItemsLinkContainer<VolunteerViewModel>()
            {
                Items = volunteer
            };
            result.AddRangeLink(CreateLinks(Method.GetAll));
            return result;
        }

        [HttpGet("{id}", Name = "GetVolunteerById")]
        [ProducesResponseType(201, Type = typeof(VolunteerViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Get(long id)
        {
            var volunteer = _volunteerAppService.GetById(id);
            if (volunteer != null)
            {
                volunteer.AddRangeLink(CreateLinks(Method.Get, volunteer));
                return Ok(volunteer);
            }
            else
                return BadRequest();
        }

        [HttpPost(Name = "InsertVolunteer")]
        [ProducesResponseType(201, Type = typeof(VolunteerViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public VolunteerViewModel Post([FromBody]VolunteerViewModel volunteer)
        {
            volunteer = _volunteerAppService.Add(volunteer);
            volunteer.AddRangeLink(CreateLinks(Method.Post, volunteer));
            return volunteer;
        }

        [HttpPut("{id}", Name = "UpdateVolunteer")]
        [ProducesResponseType(201, Type = typeof(VolunteerViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Put(long id, [FromBody]VolunteerViewModel volunteer)
        {
            if (_volunteerAppService.GetById(volunteer.Id).Id != 0)
            {
                volunteer = _volunteerAppService.Update(volunteer);
                volunteer.AddRangeLink(CreateLinks(Method.Put, volunteer));
                return Ok(volunteer);
            }
            else
                return BadRequest();
        }

        [HttpDelete("{id}", Name = "DeleteVolunteer")]
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

        private IEnumerable<Link> CreateLinks(Method method, VolunteerViewModel volunteer = null)
        {
            var linkContainer = new LinkContainer();
            if (Url != null)
            {
                var getAll = new Link() { Method = "GET", Rel = "get all volunteers", Href = Url.Link("GetAllVolunteers", new { }) };
                var insert = new Link() { Method = "POST", Rel = "insert volunteer", Href = Url.Link("InsertVolunteer", new { }) };

                var getById = new Link();
                var update = new Link();
                var delete = new Link();

                if (volunteer != null)
                {
                    getById = new Link() { Method = "GET", Rel = "get volunteer by id", Href = Url.Link("GetVolunteerById", new { id = volunteer.Id }) };
                    update = new Link() { Method = "PUT", Rel = "update volunteer", Href = Url.Link("UpdateVolunteer", new { id = volunteer.Id }) };
                    delete = new Link() { Method = "DELETE", Rel = "delete volunteer", Href = Url.Link("DeleteVolunteer", new { id = volunteer.Id }) };
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
