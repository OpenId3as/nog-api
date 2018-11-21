using System;

namespace OpenId3as.DivulgacaoONGs.Application.ViewModels.Collaborators
{
    public class CollaboratorAddressViewModel
    {
        public CollaboratorAddressViewModel()
        {

        }

        public Int64 Id { get; set; }
        public Int64 CollaboratorId { get; set; }
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

        public CollaboratorViewModel Collaborator { get; set; }
    }
}
