using System;
using System.Collections.Generic;
using System.Text;

namespace OpenId3as.DivulgacaoONGs.Domain.Entities.Page
{
    public class Banner : MongoEntity
    {
        public string Institution { get; set; }
        public List<DataPage> Data { get; set; }
    }
}
