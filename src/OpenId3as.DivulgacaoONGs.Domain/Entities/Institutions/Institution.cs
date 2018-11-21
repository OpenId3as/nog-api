using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenId3as.DivulgacaoONGs.Domain.Entities.Institutions
{
    public class Institution : PostgresEntity
    {
        public Institution()
        {

        }

        [Column("st_legal_name")]
        public string LegalName { get; set; }
        [Column("st_trade_name")]
        public string TradeName { get; set; }
        [Column("st_email")]
        public string Email { get; set; }
        [Column("dt_foundation")]
        public DateTime Foundation { get; set; }

        public ICollection<InstitutionAddress> Addresses { get; set; }
        public ICollection<InstitutionPhone> Phones { get; set; }
    }
}
