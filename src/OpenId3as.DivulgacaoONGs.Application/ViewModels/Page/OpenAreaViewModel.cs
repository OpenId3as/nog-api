using System;
using System.Collections.Generic;
using System.Text;

namespace OpenId3as.DivulgacaoONGs.Application.ViewModels.Page
{
    public class OpenAreaViewModel
    {
        public OpenAreaViewModel()
        {

        }

        public LanguageViewModel Language { get; set; }

        public LogoViewModel Logo { get; set; }
        public MenuViewModel Menu { get; set; }

        public HomeViewModel Home { get; set; }
        public WhoAreWeViewModel WhoAreWe { get; set; }
        public HowToHelpViewModel HowToHelp { get; set; }
        public ContactViewModel Contact { get; set; }
        public CollaboratorViewModel Collaborator { get; set; }
        public VolunteerViewModel Volunteer { get; set; }
    }
}
