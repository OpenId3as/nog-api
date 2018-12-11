using System.ComponentModel.DataAnnotations.Schema;

namespace OpenId3as.DivulgacaoONGs.Domain.Entities.Institutions
{
    public class InstitutionPhone : PostgresEntity
    {
        public InstitutionPhone()
        {

        }

        [Column("in_institution_id")]
        public long InstitutionId { get; set; }
        [Column("in_type")]
        public int Type { get; set; }
        [Column("st_number")]
        public string Number { get; set; }

        public Institution Institution { get; set; }
    }
}
