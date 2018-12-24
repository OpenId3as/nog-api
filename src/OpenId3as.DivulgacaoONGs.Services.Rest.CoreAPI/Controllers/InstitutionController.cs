using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using OpenId3as.DivulgacaoONGs.Application.Interfaces;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.Enum;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Institutions;
using OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Security;
using OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.HyperMedia;
using System.Linq;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Controllers
{
    [ApiVersionNeutral]
    [Route("api/[controller]")]
    [Authorize]
    public class InstitutionController : BaseController
    {
        private readonly IInstitutionAppService _institutionAppService;
        private readonly IDistributedCache _cache;
        private readonly InstitutionEnricher _institutionEnricher;

        public InstitutionController(IDistributedCache cache, IInstitutionAppService institutionAppService, IUrlHelper urlHelper)
        {
            _cache = cache;
            _institutionAppService = institutionAppService;
            _institutionEnricher = new InstitutionEnricher(urlHelper);
        }

        [HttpGet(Name = "GetAllInstitutions")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(ItemsLinkContainer<InstitutionViewModel>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ItemsLinkContainer<InstitutionViewModel> Get()
        {
            var institutions = _institutionAppService.GetAll().ToList();
            institutions.ForEach(x => x.AddRangeLink(_institutionEnricher.CreateLinks(Method.Get, x)));
            var result = new ItemsLinkContainer<InstitutionViewModel>()
            {
                Items = institutions
            };
            result.AddRangeLink(_institutionEnricher.CreateLinks(Method.GetAll));
            return result;
        }

        [HttpGet("{id}", Name = "GetInstitutionById")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(InstitutionViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Get(long id)
        {
            var institution = _institutionAppService.GetById(id);
            if (institution != null)
            {
                institution.AddRangeLink(_institutionEnricher.CreateLinks(Method.Get, institution));
                return Ok(institution);
            }
            else
                return BadRequest();
        }

        [HttpPost(Name = "InsertInstitution")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(InstitutionViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public InstitutionViewModel Post([FromBody]InstitutionViewModel institution)
        {
            institution = _institutionAppService.Add(institution);
            institution.AddRangeLink(_institutionEnricher.CreateLinks(Method.Post, institution));
            return institution;
        }

        [HttpPut("{id}", Name = "UpdateInstitution")]
        [Authorize(AuthorizationNameOptions.Bearer)]
        [ProducesResponseType(200, Type = typeof(InstitutionViewModel))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Put(long id, [FromBody]InstitutionViewModel institution)
        {
            if (_institutionAppService.GetById(institution.Id).Id != 0)
            {
                institution = _institutionAppService.Update(institution);
                institution.AddRangeLink(_institutionEnricher.CreateLinks(Method.Put, institution));
                return Ok(institution);
            }
            else
                return BadRequest();
        }

        [HttpDelete("{id}", Name = "DeleteInstitution")]
        [Authorize(AuthorizationNameOptions.Bearer)]
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
    }
}
