using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenId3as.DivulgacaoONGs.Domain.Entities.Collaborators
{
    public class CollaboratorAddress : PostgresEntity
    {
        public CollaboratorAddress()
        {

        }

        [Column("in_collaborator_id")]
        public Int64 CollaboratorId { get; set; }
        [Column("in_postal_code")]
        public int PostalCode { get; set; }
        [Column("st_name")]
        public string Name { get; set; }
        [Column("in_number")]
        public int Number { get; set; }
        [Column("st_neighborhood")]
        public string Neighborhood { get; set; }
        [Column("st_city")]
        public string City { get; set; }
        [Column("st_federated_state")]
        public string FederatedState { get; set; }
        [Column("st_complement")]
        public string Complement { get; set; }

        public Collaborator Collaborator { get; set; }
    }
}
