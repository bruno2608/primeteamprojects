using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;

namespace PrimeTeamProjectsApi
{
    /// <summary>
    /// Classe responsável pelo início da Aplicação.
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {

        /// <summary>
        /// Início da aplicação.
        /// </summary>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        /// <summary>
        /// Controle de Sessão.
        /// </summary>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        protected void Application_PostAuthorizeRequest()
        {
            if (HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith("~/api"))
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            }
        }

    }
}
