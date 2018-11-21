using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.ViewModels.Collaborators
{
    public class CollaboratorViewModel
    {
        public CollaboratorViewModel()
        {

        }

        public Int64 Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Birth { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool Active { get; set; }

        public List<CollaboratorAddressViewModel> Addresses { get; set; }
        public List<CollaboratorPhoneViewModel> Phones { get; set; }
    }
}
