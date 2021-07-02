using Newtonsoft.Json;

namespace CaioOliveira.IBGE.Models
{
    public class Microregion
    {
        public int Id { get; set; }
        [JsonProperty("nome")]
        public string Name { get; set; }
        [JsonProperty("mesorregiao")]
        public Mesoregion Mesoregion { get; set; }
    }
}