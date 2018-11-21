using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenId3as.DivulgacaoONGs.Domain.Entities
{
    public class PostgresEntity
    {
        [Column("in_id")]
        public Int64 Id { get; set; }
        [Column("ts_created")]
        public DateTime Created { get; set; }
        [Column("ts_updated")]
        public DateTime Updated { get; set; }
        [Column("bo_active")]
        public bool Active { get; set; }
    }
}
