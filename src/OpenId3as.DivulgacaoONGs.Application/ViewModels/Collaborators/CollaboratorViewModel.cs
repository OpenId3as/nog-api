using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace OpenId3as.DivulgacaoONGs.Application.ViewModels.Collaborators
{
    [DataContract]
    public class CollaboratorViewModel : LinkContainer
    {
        public CollaboratorViewModel()
        {
        }

        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public DateTime Birth { get; set; }
        [DataMember]
        public DateTime Created { get; set; }
        [DataMember]
        public DateTime Updated { get; set; }
        [DataMember]
        public bool Active { get; set; }

        [DataMember]
        public List<CollaboratorAddressViewModel> Addresses { get; set; }
        [DataMember]
        public List<CollaboratorPhoneViewModel> Phones { get; set; }
    }
}
