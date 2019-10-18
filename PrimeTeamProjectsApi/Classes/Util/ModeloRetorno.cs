using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrimeTeamProjectsApi
{
    /// <summary>
    /// Modelo de retorno padrão.
    /// </summary>
    public class ModeloRetorno
    {
        /// <summary>
        /// Código do retorno.
        /// </summary>
        public int codigo { get; set; }
        /// <summary>
        /// Mensagem de retorno.
        /// </summary>
        public string mensagem { get; set; }
    }

    /// <summary>
    /// Modelo de retorno padrão com objeto.
    /// </summary>
    public class ModeloRetorno<T>
    {
        /// <summary>
        /// Código do retorno.
        /// </summary>
        public int codigo { get; set; }
        /// <summary>
        /// Mensagem de retorno.
        /// </summary>
        public string mensagem { get; set; }
        /// <summary>
        /// Objeto de retorno.
        /// </summary>
        public T objeto { get; set; }
    }
}