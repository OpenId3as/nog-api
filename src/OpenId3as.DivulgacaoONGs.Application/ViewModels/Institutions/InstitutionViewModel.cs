using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.ViewModels.Institutions
{
    public class InstitutionViewModel : LinkContainer
    {
        public InstitutionViewModel()
        {

        }

        public long Id { get; set; }
        public string LegalName { get; set; }
        public string TradeName { get; set; }
        public string Email { get; set; }
        public DateTime Foundation { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool Active { get; set; }

        public List<InstitutionAddressViewModel> Addresses { get; set; }
        public List<InstitutionPhoneViewModel> Phones { get; set; }
    }
}
