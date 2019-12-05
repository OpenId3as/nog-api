using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenId3as.DivulgacaoONGs.Application.Interfaces.Page;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.Enum;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.HyperMedia;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiVersionNeutral]
    [ApiController]
    public class OpenAreaController : OpenAreaControllerBase
    {
        private readonly ICollaboratorAppService _collaboratorPageAppService;
        private readonly IContactAppService _contactAppService;
        private readonly IHomeAppService _homeAppService;
        private readonly IHowToHelpAppService _howToHelpAppService;
        private readonly ILanguageAppService _languageAppService;
        private readonly ILogoAppService _logoAppService;
        private readonly IMenuAppService _menuAppService;
        private readonly IVolunteerAppService _volunteerAppService;
        private readonly IWhoAreWeAppService _whoAreWeAppService;

        private readonly LanguageEnricher _languageEnricher;
        private readonly LogoEnricher _logoEnricher;
        private readonly MenuEnricher _menuEnricher;

        private readonly CollaboratorPageEnricher _collaboratorEnricher;
        private readonly ContactEnricher _contactEnricher;
        private readonly HomeEnricher _homeEnricher;
        private readonly HowToHelpEnricher _howToHelpEnricher;
        private readonly VolunteerPageEnricher _volunteerEnricher;
        private readonly WhoAreWeEnricher _whoAreWeEnricher;

        public OpenAreaController(
            ICollaboratorAppService collaboratorPageAppService,
            IContactAppService contactAppService,
            IHomeAppService homeAppService,
            IHowToHelpAppService howToHelpAppService,
            ILanguageAppService languageAppService,
            ILogoAppService logoAppService,
            IMenuAppService menuAppService,
            IVolunteerAppService volunteerAppService,
            IWhoAreWeAppService whoAreWeAppService,
            IUrlHelper urlHelper)
        {
            _collaboratorPageAppService = collaboratorPageAppService;
            _contactAppService = contactAppService;
            _homeAppService = homeAppService;
            _howToHelpAppService = howToHelpAppService;
            _languageAppService = languageAppService;
            _logoAppService = logoAppService;
            _menuAppService = menuAppService;
            _volunteerAppService = volunteerAppService;
            _whoAreWeAppService = whoAreWeAppService;

            _languageEnricher = new LanguageEnricher(urlHelper);
            _logoEnricher = new LogoEnricher(urlHelper);
            _menuEnricher = new MenuEnricher(urlHelper);

            _collaboratorEnricher = new CollaboratorPageEnricher(urlHelper);
            _contactEnricher = new ContactEnricher(urlHelper);
            _homeEnricher = new HomeEnricher(urlHelper);
            _howToHelpEnricher = new HowToHelpEnricher(urlHelper);
            _volunteerEnricher = new VolunteerPageEnricher(urlHelper);
            _whoAreWeEnricher = new WhoAreWeEnricher(urlHelper);
        }

        [HttpGet("{language}/{institution}", Name = "OpenArea")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(OpenAreaViewModel))]
        public OpenAreaViewModel Get(string language, string institution)
        {
            var openArea = new OpenAreaViewModel();

            openArea.Language = _languageAppService.GetByLang(language);
            openArea.Logo = _logoAppService.GetByInstitution(institution);
            openArea.Menu = _menuAppService.GetInstitutionByLanguage(language, institution);

            openArea.Collaborator = _collaboratorPageAppService.GetInstitutionByLanguage(language, institution);
            openArea.Contact = _contactAppService.GetInstitutionByLanguage(language, institution);
            openArea.Home = _homeAppService.GetInstitutionByLanguage(language, institution);
            openArea.HowToHelp = _howToHelpAppService.GetInstitutionByLanguage(language, institution);
            openArea.Volunteer = _volunteerAppService.GetInstitutionByLanguage(language, institution);
            openArea.WhoAreWe = _whoAreWeAppService.GetInstitutionByLanguage(language, institution);

            return openArea;
        }

        [HttpGet("language/{language}", Name = "OpenAreaLanguage")]
        [ProducesResponseType(200, Type = typeof(LanguageViewModel))]
        [ProducesResponseType(404)]
        public IActionResult GetLanguage(string language)
        {
            var lang = _languageAppService.GetByLang(language);
            lang?.AddRangeLink(_languageEnricher.CreateLinks(Method.Get, lang));
            return OkOrNotFound(lang);
        }

        [HttpGet("logo/{institution}", Name = "OpenAreaLogo")]
        [ProducesResponseType(200, Type = typeof(LogoViewModel))]
        [ProducesResponseType(404)]
        public IActionResult GetLogo(string institution)
        {
            var logo = _logoAppService.GetByInstitution(institution);
            logo?.AddRangeLink(_logoEnricher.CreateLinks(Method.Get, logo));
            return OkOrNotFound(logo);
        }

        [HttpGet("menu/{language}/{institution}", Name = "OpenAreaMenu")]
        [ProducesResponseType(200, Type = typeof(MenuViewModel))]
        [ProducesResponseType(404)]
        public IActionResult GetMenu(string language, string institution)
        {
            var menu = _menuAppService.GetInstitutionByLanguage(language, institution);
            menu?.AddRangeLink(_menuEnricher.CreateLinks(Method.Get, menu));
            return OkOrNotFound(menu);
        }

        [HttpGet("collaborator/{language}/{institution}", Name = "OpenAreaCollaborator")]
        [ProducesResponseType(200, Type = typeof(CollaboratorViewModel))]
        [ProducesResponseType(404)]
        public IActionResult GetCollaborator(string language, string institution)
        {
            var collaborator = _collaboratorPageAppService.GetInstitutionByLanguage(language, institution);
            collaborator?.AddRangeLink(_collaboratorEnricher.CreateLinks(Method.Get, collaborator));
            return OkOrNotFound(collaborator);
        }

        [HttpGet("contact/{language}/{institution}", Name = "OpenAreaContact")]
        [ProducesResponseType(200, Type = typeof(ContactViewModel))]
        [ProducesResponseType(404)]
        public IActionResult GetContact(string language, string institution)
        {
            var contact = _contactAppService.GetInstitutionByLanguage(language, institution);
            contact?.AddRangeLink(_contactEnricher.CreateLinks(Method.Get, contact));
            return OkOrNotFound(contact);
        }

        [HttpGet("home/{language}/{institution}", Name = "OpenAreaHome")]
        [ProducesResponseType(200, Type = typeof(HomeViewModel))]
        [ProducesResponseType(404)]
        public IActionResult GetHome(string language, string institution)
        {
            var home = _homeAppService.GetInstitutionByLanguage(language, institution);
            home?.AddRangeLink(_homeEnricher.CreateLinks(Method.Get, home));
            return OkOrNotFound(home);
        }

        [HttpGet("howtohelp/{language}/{institution}", Name = "OpenAreaHowToHelp")]
        [ProducesResponseType(200, Type = typeof(HowToHelpViewModel))]
        [ProducesResponseType(404)]
        public IActionResult GetHowToHelp(string language, string institution)
        {
            var howToHelp = _howToHelpAppService.GetInstitutionByLanguage(language, institution);
            howToHelp?.AddRangeLink(_howToHelpEnricher.CreateLinks(Method.Get, howToHelp));
            return OkOrNotFound(howToHelp);
        }

        [HttpGet("volunteer/{language}/{institution}", Name = "OpenAreaVolunteer")]
        [ProducesResponseType(200, Type = typeof(VolunteerViewModel))]
        [ProducesResponseType(404)]
        public IActionResult GetVolunteer(string language, string institution)
        {
            var volunteer = _volunteerAppService.GetInstitutionByLanguage(language, institution);
            volunteer?.AddRangeLink(_volunteerEnricher.CreateLinks(Method.Get, volunteer));
            return OkOrNotFound(volunteer);
        }

        [HttpGet("whoarewe/{language}/{institution}", Name = "OpenAreaWhoAreWe")]
        [ProducesResponseType(200, Type = typeof(WhoAreWeViewModel))]
        [ProducesResponseType(404)]
        public IActionResult GetWhoAreWe(string language, string institution)
        {
            var whoAreWe = _whoAreWeAppService.GetInstitutionByLanguage(language, institution);
            whoAreWe?.AddRangeLink(_whoAreWeEnricher.CreateLinks(Method.Get, whoAreWe));
            return OkOrNotFound(whoAreWe);
        }
    }
}
