using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenId3as.DivulgacaoONGs.Domain.Entities.Volunteers
{
    public class Volunteer : PostgresEntity
    {
        public Volunteer()
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

        public ICollection<VolunteerAddress> Addresses { get; set; }
        public ICollection<VolunteerPhone> Phones { get; set; }
    }
}
