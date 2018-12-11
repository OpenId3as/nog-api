using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using OpenId3as.DivulgacaoONGs.Application.Interfaces;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.Enum;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Institutions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Controllers
{
    [ApiVersionNeutral]
    [Route("api/[controller]")]
    public class InstitutionController : BaseController
    {
        private readonly IInstitutionAppService _institutionAppService;
        private readonly IDistributedCache _cache;
        public InstitutionController(IDistributedCache cache, IInstitutionAppService institutionAppService)
        {
            _cache = cache;
            _institutionAppService = institutionAppService;
        }

        [HttpGet(Name = "GetAllInstitutions")]
        [ProducesResponseType(201, Type = typeof(ItemsLinkContainer<InstitutionViewModel>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ItemsLinkContainer<InstitutionViewModel> Get()
        {
            var institutions = _institutionAppService.GetAll().ToList();
            institutions.ForEach(x => x.AddRangeLink(CreateLinks(Method.Get, x)));
            var result = new ItemsLinkContainer<InstitutionViewModel>()
            {
                Items = institutions
            };
            result.AddRangeLink(CreateLinks(Method.GetAll));
            return result;
        }

        [HttpGet("{id}", Name = "GetInstitutionById")]
        [ProducesResponseType(201, Type = typeof(InstitutionViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Get(long id)
        {
            var institution = _institutionAppService.GetById(id);
            if (institution != null)
            {
                institution.AddRangeLink(CreateLinks(Method.Get, institution));
                return Ok(institution);
            }
            else
                return BadRequest();
        }

        [HttpPost(Name = "InsertInstitution")]
        [ProducesResponseType(201, Type = typeof(InstitutionViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public InstitutionViewModel Post([FromBody]InstitutionViewModel institution)
        {
            institution = _institutionAppService.Add(institution);
            institution.AddRangeLink(CreateLinks(Method.Post, institution));
            return institution;
        }

        [HttpPut("{id}", Name = "UpdateInstitution")]
        [ProducesResponseType(201, Type = typeof(InstitutionViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Put(long id, [FromBody]InstitutionViewModel institution)
        {
            if (_institutionAppService.GetById(institution.Id).Id != 0)
            {
                institution = _institutionAppService.Update(institution);
                institution.AddRangeLink(CreateLinks(Method.Put, institution));
                return Ok(institution);
            }
            else
                return BadRequest();
        }

        [HttpDelete("{id}", Name = "DeleteInstitution")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Delete(long id)
        {
            if (_institutionAppService.GetById(id).Id != 0)
            {
                _institutionAppService.Delete(id);
                return NoContent();
            }
            else
                return BadRequest();
        }

        private IEnumerable<Link> CreateLinks(Method method, InstitutionViewModel institution = null)
        {
            var linkContainer = new LinkContainer();
            if (Url != null)
            {
                var getAll = new Link() { Method = "GET", Rel = "get all institutions", Href = Url.Link("GetAllInstitutions", new { }) };
                var insert = new Link() { Method = "POST", Rel = "insert institution", Href = Url.Link("InsertInstitution", new { }) };

                var getById = new Link();
                var update = new Link();
                var delete = new Link();

                if (institution != null)
                {
                    getById = new Link() { Method = "GET", Rel = "get institution by id", Href = Url.Link("GetInstitutionById", new { id = institution.Id }) };
                    update = new Link() { Method = "PUT", Rel = "update institution", Href = Url.Link("UpdateInstitution", new { id = institution.Id }) };
                    delete = new Link() { Method = "DELETE", Rel = "delete institution", Href = Url.Link("DeleteInstitution", new { id = institution.Id }) };
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
