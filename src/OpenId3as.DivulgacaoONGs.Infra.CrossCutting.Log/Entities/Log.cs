using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Entities
{
    public class Log
    {
        public Log()
        {
            Created = DateTime.Now;
        }

        [Column("in_id")]
        public long Id { get; set; }
        [Column("in_event_id")]
        public int? EventId { get; set; }
        [Column("st_log_level")]
        public string LogLevel { get; set; }
        [Column("st_message")]
        public string Message { get; set; }
        [Column("ts_created")]
        public DateTime Created { get; set; }
    }
}
