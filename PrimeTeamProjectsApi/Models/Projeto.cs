using System;

namespace PrimeTeamProjectsApi.Models
{
    /// <summary>
    /// Modelo da tabela.
    /// </summary>
    public class Projeto
    {
        /// <summary>
        /// Código do projeto.
        /// </summary>
        public int CODPRJ { get; set; }
        /// <summary>
        /// Nome do projeto.
        /// </summary>
        public string NOMPRJ { get; set; }
        /// <summary>
        /// Descrição do projeto.
        /// </summary>
        public string DESPRJ { get; set; }
        /// <summary>
        /// Data de início do projeto.
        /// </summary>
        public DateTime DATINIPRJ { get; set; }
        /// <summary>
        /// Data de término do projeto.
        /// </summary>
        public DateTime DATFIMPRJ { get; set; }
        /// <summary>
        /// Data de início do projeto. (formatada)
        /// </summary>
        public string DATINISTR { get; set; }
        /// <summary>
        /// Data de término do projeto. (formatada)
        /// </summary>
        public string DATFIMSTR { get; set; }
        /// <summary>
        /// Status do projeto. (Coluna virtual)
        /// </summary>
        public string STATUS { get; set; }
    }
}