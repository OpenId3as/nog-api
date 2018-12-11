using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.ViewModels.Volunteers
{
    public class VolunteerViewModel : LinkContainer
    {
        public VolunteerViewModel()
        {

        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Birth { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool Active { get; set; }

        public List<VolunteerAddressViewModel> Addresses { get; set; }
        public List<VolunteerPhoneViewModel> Phones { get; set; }
    }
}
