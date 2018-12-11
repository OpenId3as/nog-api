using System.ComponentModel.DataAnnotations.Schema;

namespace OpenId3as.DivulgacaoONGs.Domain.Entities.Volunteers
{
    public class VolunteerAddress : PostgresEntity
    {
        public VolunteerAddress()
        {

        }

        [Column("in_volunteer_id")]
        public long VolunteerId { get; set; }
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

        public Volunteer Volunteer { get; set; }
    }
}
