using System;

namespace OpenId3as.DivulgacaoONGs.Application.ViewModels.Institutions
{
    public class InstitutionPhoneViewModel
    {
        public InstitutionPhoneViewModel()
        {

        }

        public Int64 Id { get; set; }
        public Int64 InstitutionId { get; set; }
        public int Type { get; set; }
        public string Number { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool Active { get; set; }

        public InstitutionViewModel Institution { get; set; }
    }
}
