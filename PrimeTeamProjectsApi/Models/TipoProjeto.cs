using System;
using System.Linq;
using System.Collections.Generic;

namespace PrimeTeamProjectsApi.Models
{
    /// <summary>
    /// Modelo da tabela.
    /// </summary>
    public class TipoProjeto
    {
        /// <summary>
        /// Código do tipo de projeto.
        /// </summary>
        public int CODTIP { get; set; }
        /// <summary>
        /// Nome do tipo de projeto.
        /// </summary>
        public string NOMTIP { get; set; }
        /// <summary>
        /// Descrição do tipo de projeto.
        /// </summary>
        public string DESTIP { get; set; }
        /// <summary>
        /// Indica se a relação com o projeto foi removida ou inserida. 
        /// (uso do programa: 0 normal, 1 inserida, 2 removida)
        /// </summary>
        public int STATUSREL { get; set; } = 0;
    }
}