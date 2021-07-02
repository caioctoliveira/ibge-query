using Newtonsoft.Json;

namespace CaioOliveira.IBGE.Models
{
    public class Mesoregion
    {
        public int Id { get; set; }
        
        [JsonProperty("nome")]
        public string Name { get; set; }
     
        public UF UF { get; set; }
    }
}