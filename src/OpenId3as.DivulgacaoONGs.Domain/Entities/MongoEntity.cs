using System;

namespace OpenId3as.DivulgacaoONGs.Domain.Entities
{
    public class MongoEntity
    {
        public long Id { get; set; }

        public MongoEntity()
        {
            Id = Guid.NewGuid().GetHashCode();
        }

        public bool IsTransient()
        {
            return this.Id == default(long);
        }
    }
}
