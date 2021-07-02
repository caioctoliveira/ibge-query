using Newtonsoft.Json;

namespace CaioOliveira.IBGE.Models
{
    public class Region
    {
        public int Id { get; set; }

        [JsonProperty("sigla")]
        public string Initial { get; set; }
        
        [JsonProperty("nome")]
        public string Name { get; set; }
    }
}