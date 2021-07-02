using Newtonsoft.Json;

namespace CaioOliveira.IBGE.Models
{
    public class City
    {
        public int Id { get; set; }
        [JsonProperty("nome")]
        public string Name { get; set; }
        [JsonProperty("microrregiao")]
        public Microregion Microregion { get; set; }
    }
}
