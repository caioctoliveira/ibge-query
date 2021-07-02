namespace CaioOliveira.IBGE.Configuration
{
    public class ServiceConfiguration
    {
        public string BaseApiUrl { get; set; }

        internal void Bind(ServiceConfiguration opt)
        {
            this.BaseApiUrl = opt.BaseApiUrl;
        }
    }
}