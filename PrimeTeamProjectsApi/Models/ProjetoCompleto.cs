using System;
using System.Linq;
using System.Collections.Generic;

namespace PrimeTeamProjectsApi.Models
{
    /// <summary>
    /// Modelo dos dados completos do projeto.
    /// </summary>
    public class ProjetoCompleto
    {
        /// <summary>
        /// Dados básicos do projeto.
        /// </summary>
        public Projeto projeto { get; set; }
        /// <summary>
        /// Tipos do projeto.
        /// </summary>
        public List<TipoProjeto> tipos { get; set; }
        /// <summary>
        /// Relações Atividade x Usuário x Projeto.
        /// </summary>
        public List<RelacaoAUP> relacoesAUP { get; set; }
        /// <summary>
        /// Lista de Usuários.
        /// </summary>
        public List<Usuario> usuarios { get; set; }
    }
}