using System;
using System.Web.Http;
using System.Web.Http.Cors;
using Swashbuckle.Application;

namespace PrimeTeamProjectsApi
{
    /// <summary>
    /// Classe de configuração da api.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registrador das configurações/serviços da api.
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Configurando cors.
            EnableCorsAttribute cors = new EnableCorsAttribute(origins: "http://localhost:4200", headers: "*", methods: "*");
            // Rotas da Web API.
            config.MapHttpAttributeRoutes();
            // Habilitando cors.
            config.EnableCors(cors);
            // Configurando swagger na rota.
            config.Routes.MapHttpRoute(
                name: "Swagger UI",
                routeTemplate: string.Empty,
                defaults: null,
                constraints: null,
                handler: new RedirectHandler(SwaggerDocsConfig.DefaultRootUrlResolver, "swagger/ui/index")
            );
        }
    }
}
