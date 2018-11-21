using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenId3as.DivulgacaoONGs.Domain.Entities.Collaborators
{
    public class Collaborator : PostgresEntity
    {
        public Collaborator()
        {

        }

        [Column("st_first_name")]
        public string FirstName { get; set; }
        [Column("st_last_name")]
        public string LastName { get; set; }
        [Column("st_email")]
        public string Email { get; set; }
        [Column("dt_birth")]
        public DateTime Birth { get; set; }

        public ICollection<CollaboratorAddress> Addresses { get; set; }
        public ICollection<CollaboratorPhone> Phones { get; set; }
    }
}
