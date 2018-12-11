using System;

namespace OpenId3as.DivulgacaoONGs.Application.ViewModels.Collaborators
{
    public class CollaboratorPhoneViewModel
    {
        public CollaboratorPhoneViewModel()
        {

        }

        public long Id { get; set; }
        public long CollaboratorId { get; set; }
        public int Type { get; set; }
        public string Number { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool Active { get; set; }

        public CollaboratorViewModel Collaborator { get; set; }
    }
}
