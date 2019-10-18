using System;
using System.Collections.Generic;

namespace PrimeTeamProjectsApi.Models
{
    /// <summary>
    /// Modelo da tabela.
    /// </summary>
    /// <remarks>Leon Denis Paiva e Silva</remarks>
    public class Usuario
    {
        /// <summary>
        /// Códigigo do usuário.
        /// </summary>
        public int CODUSR { get; set; }
        /// <summary>
        /// Id do usuário.
        /// </summary>
        public string USRID { get; set; }
        /// <summary>
        /// Senha do usuário.
        /// </summary>
        public string USRPSW { get; set; }
        /// <summary>
        /// Nome do usuário.
        /// </summary>
        public string NOMUSR { get; set; }
        /// <summary>
        /// Email do usuário.
        /// </summary>
        public string USRMAIL { get; set; }
        /// <summary>
        /// Tipo do usuário.
        /// </summary>
        public short TIPUSR { get; set; }
        /// <summary>
        /// Data de cadastro do usuário.
        /// </summary>
        public DateTime DATCAD { get; set; }
        /// <summary>
        /// Data de desativação do usuário.
        /// </summary>
        public DateTime DATDST { get; set; }
        /// <summary>
        /// Status do usuário. (Ativado|Desativado)(Coluna virtual)
        /// </summary>
        public string STATUS { get; set; }        
        /// <summary>
        /// Atividades do usuário. (Auxíliar)
        /// </summary>
        public List<Atividade> atividades { get; set; }
    }
}