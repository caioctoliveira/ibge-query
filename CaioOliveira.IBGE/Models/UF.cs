using Newtonsoft.Json;

namespace CaioOliveira.IBGE.Models
{
    public class UF
    {
        public int Id { get; set; }

        [JsonProperty("nome")]
        public string Name { get; set; }
        
        [JsonProperty("sigla")]
        public string Initials { get; set; }
        
        [JsonProperty("regiao")]
        public Region Region { get; set; }
    }
}