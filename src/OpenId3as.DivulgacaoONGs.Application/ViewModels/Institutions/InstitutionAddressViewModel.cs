using System;

namespace OpenId3as.DivulgacaoONGs.Application.ViewModels.Institutions
{
    public class InstitutionAddressViewModel
    {
        public InstitutionAddressViewModel()
        {

        }

        public long Id { get; set; }
        public long InstitutionId { get; set; }
        public int PostalCode { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string FederatedState { get; set; }
        public string Complement { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool Active { get; set; }

        public InstitutionViewModel Institution { get; set; }
    }
}
