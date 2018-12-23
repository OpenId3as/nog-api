using System.ComponentModel.DataAnnotations.Schema;

namespace OpenId3as.DivulgacaoONGs.Domain.Entities
{
    public class User : PostgresEntity
    {
        public User()
        {

        }

        [Column("st_login")]
        public string Login { get; set; }
        [Column("st_password")]
        public string Password { get; set; }
        [Column("st_first_name")]
        public string FirstName { get; set; }
        [Column("st_last_name")]
        public string LastName { get; set; }
        [Column("st_email")]
        public string Email { get; set; }
        [Column("bo_first_access")]
        public bool FirstAccess { get; set; }
    }
}
