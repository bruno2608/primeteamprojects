using System;
using System.Linq;
using System.Collections.Generic;

namespace PrimeTeamProjectsApi.Models
{
    /// <summary>
    /// Modelo do Select.
    /// </summary>
    public class RelacaoAUP
    {
        /// <summary>
        /// Código da relação Atividade x Usuário x Projeto.
        /// </summary>
        public int CODATVUSRPRJ { get; set; }
        /// <summary>
        /// Código do projeto.
        /// </summary>
        public int CODPRJ { get; set; }
        /// <summary>
        /// Código do usuário.
        /// </summary>
        public int CODUSR { get; set; }
        /// <summary>
        /// Nome do usuário.
        /// </summary>
        public string NOMUSR { get; set; }
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
        /// Data de início da atividade. (formatada)
        /// </summary>
        public string DATINIATVSTR { get; set; }
        /// <summary>
        /// Data de fim da atividade. (formatada)
        /// </summary>
        public string DATFIMATVSTR { get; set; }
        /// <summary>
        /// Tempo trabalhado na atividade. (segundos)
        /// </summary>
        public long TMPESTATV { get; set; }
        /// <summary>
        /// Indica se a relação foi removida ou inserida. 
        /// (uso do programa: 0 normal, 1 inserida, 2 removida)
        /// </summary>
        public int STATUSREL { get; set; } = 0;
    }
}