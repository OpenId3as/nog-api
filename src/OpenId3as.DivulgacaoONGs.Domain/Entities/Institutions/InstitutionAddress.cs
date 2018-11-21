using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenId3as.DivulgacaoONGs.Domain.Entities.Institutions
{
    public class InstitutionAddress : PostgresEntity
    {
        public InstitutionAddress()
        {

        }

        [Column("in_institution_id")]
        public Int64 InstitutionId { get; set; }
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

        public Institution Institution { get; set; }
    }
}
