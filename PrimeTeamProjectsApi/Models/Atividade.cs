using System;
using System.Collections.Generic;

namespace PrimeTeamProjectsApi.Models
{
    /// <summary>
    /// Modelo da tabela.
    /// </summary>
    public class Atividade
    {
        /// <summary>
        /// Código da atividade.
        /// </summary>
        public int CODATV { get; set; }
        /// <summary>
        /// Nome da atividade.
        /// </summary>
        public string NOMATV { get; set; }
        /// <summary>
        /// Descrição da atividade.
        /// </summary>
        public string DESATV { get; set; }
        /// <summary>
        /// Tempo trabalhado na atividade. (segundos)
        /// </summary>
        public long TMPESTATV { get; set; }
        /// <summary>
        /// Data de início da atividade.
        /// </summary>
        public DateTime DATINIATV { get; set; }
        /// <summary>
        /// Data de fim da atividade.
        /// </summary>
        public DateTime DATFIMATV { get; set; }
        /// <summary>
        /// Data de início da atividade. (formatada)
        /// </summary>
        public string DATINIATVSTR { get; set; }
        /// <summary>
        /// Data de fim da atividade. (formatada)
        /// </summary>
        public string DATFIMATVSTR { get; set; }
        /// <summary>
        /// Tempo da atividade. (formatato)
        /// </summary>
        public string TEMPOATV {
            get {
                // Criando data.
                DateTime date = new DateTime();
                // Retornando.
                return date.AddSeconds(this.TMPESTATV).ToString("HH:mm:ss");
            }
        }
        /// <summary>
        /// Status da atividade. (coluna virtual)
        /// </summary>
        public string STATUS { get; set; }
    }
}