namespace OpenId3as.DivulgacaoONGs.Domain.Entities.Page
{
    public class Logo : MongoEntity
    {
        public string Institution { get; set; }
        public DataPage Data { get; set; }
    }
}
