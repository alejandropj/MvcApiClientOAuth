using System.Net.Http.Headers;

namespace MvcApiClientOAuth.Services
{
    public class ServiceApiEmpleados
    {
        private string UrlApiEmpleados;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceApiEmpleados(IConfiguration configuration)
        {
            this.UrlApiEmpleados = 
                configuration.GetValue<string>("ApiUrls:ApiEmpleados");
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");

        }
    }
}
