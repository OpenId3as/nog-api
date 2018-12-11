using System.ComponentModel.DataAnnotations.Schema;

namespace OpenId3as.DivulgacaoONGs.Domain.Entities.Volunteers
{
    public class VolunteerPhone : PostgresEntity
    {
        public VolunteerPhone()
        {

        }

        [Column("in_volunteer_id")]
        public long VolunteerId { get; set; }
        [Column("in_type")]
        public int Type { get; set; }
        [Column("st_number")]
        public string Number { get; set; }

        public Volunteer Volunteer { get; set; }
    }
}
