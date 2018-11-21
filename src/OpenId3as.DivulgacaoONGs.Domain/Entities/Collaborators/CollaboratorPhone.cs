using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenId3as.DivulgacaoONGs.Domain.Entities.Collaborators
{
    public class CollaboratorPhone : PostgresEntity
    {
        public CollaboratorPhone()
        {

        }

        [Column("in_collaborator_id")]
        public Int64 CollaboratorId { get; set; }
        [Column("in_type")]
        public int Type { get; set; }
        [Column("st_number")]
        public string Number { get; set; }

        public Collaborator Collaborator { get; set; }
    }
}
