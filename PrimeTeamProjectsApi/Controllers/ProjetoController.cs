using System;
using System.Web;
using System.Linq;
using System.Web.Http;
using System.Collections.Generic;
using PrimeTeamProjectsApi.Models;
using PrimeTeamProjectsApi.Business;

namespace PrimeTeamProjectsApi.Controllers
{
    /// <summary>
    /// Controller da página de cadastro de projetos.
    /// </summary>
    [RoutePrefix("api/Projeto")]
    public class ProjetoController : PrimeTeamController
    {
        #region Projeto
        /// <summary>
        /// Obtem projetos cadastrados.
        /// </summary>
        /// <param name="codPrj">Código do projeto.</param>
        /// <param name="nomPrj">Nome do projeto.</param>
        /// <returns>Lista de Projetos.</returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        [HttpGet, Route("Obter")]
        public IHttpActionResult Obter(int codPrj, string nomPrj)
        {
            // Modelo de retorno.
            ModeloRetorno<List<Projeto>> retorno = null;
            // BLL.
            ProjetoBLL bll = null;
            // Tentativa.
            try
            {
                // Instanciando bll.
                bll = new ProjetoBLL();
                // Obtendo projetos.
                retorno = new ModeloRetorno<List<Projeto>>()
                {
                    codigo = 0,
                    objeto = bll.ObterProjetos(codPrj, nomPrj)
                };
            }
            catch (Exception ex)
            {
                retorno = new ModeloRetorno<List<Projeto>>()
                {
                    codigo = 1,
                    mensagem = $"Erro ao obter projetos: {ex.Message}"
                };
            }
            finally
            {
                // Limpando variável.
                bll = null;
            }
            // Retornando.
            return Ok(retorno);
        }

        /// <summary>
        /// Obtem os dados completos do projeto.
        /// </summary>
        /// <param name="codPrj">Código do projeto.</param>
        /// <returns>Todos os dados do projeto.</returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        [HttpGet, Route("ObterProjetoCompleto")]
        public IHttpActionResult ObterProjetoCompleto(int codPrj)
        {
            // Modelo de retorno.
            ModeloRetorno<ProjetoCompleto> retorno = null;
            // BLL.
            ProjetoBLL bll = null;
            // Tentativa.
            try
            {
                // Instanciando bll.
                bll = new ProjetoBLL();
                // Obtendo projeto.
                retorno = new ModeloRetorno<ProjetoCompleto>()
                {
                    codigo = 0,
                    objeto = bll.ObterProjetoCompleto(codPrj)
                };
            }
            catch (Exception ex)
            {
                retorno = new ModeloRetorno<ProjetoCompleto>()
                {
                    codigo = 1,
                    mensagem = $"Erro ao obter projeto: {ex.Message}"
                };
            }
            finally
            {
                // Limpando variável.
                bll = null;
            }
            // Retornando.
            return Ok(retorno);
        }

        /// <summary>
        /// Insere um novo projeto.
        /// </summary>
        /// <param name="projetoCompleto">Dados completos do projeto.</param>
        /// <returns>Modelo Retorno.</returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        [HttpPost, Route("Inserir")]
        public IHttpActionResult Inserir(ProjetoCompleto projetoCompleto)
        {
            // Modelo de retorno.
            ModeloRetorno retorno = null;
            // BLL.
            ProjetoBLL bll = null;
            // Tentativa.
            try
            {
                // Instanciando bll.
                bll = new ProjetoBLL();
                // Inserindo projeto.
                if (bll.InserirProjeto(projetoCompleto))
                    retorno = new ModeloRetorno()
                    {
                        codigo = 0,
                        mensagem = $"Projeto '{projetoCompleto.projeto.NOMPRJ}' inserido com sucesso."
                    };
                else
                    retorno = new ModeloRetorno()
                    {
                        codigo = 2,
                        mensagem = "Erro ao inserir projeto. (Algumas tabelas não foram afetadas)"
                    };
            }
            catch (Exception ex)
            {
                retorno = new ModeloRetorno()
                {
                    codigo = 1,
                    mensagem = $"Erro ao inserir projeto: {ex.Message}"
                };
            }
            finally
            {
                // Limpando variável.
                bll = null;
            }
            // Retornando.
            return Ok(retorno);
        }


        /// <summary>
        /// Atualiza os dados do projeto.
        /// </summary>
        /// <param name="projetoCompleto">Dados completos do projeto.</param>
        /// <returns>Modelo Retorno.</returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        [HttpPost, Route("Atualizar")]
        public IHttpActionResult Atualizar(ProjetoCompleto projetoCompleto)
        {
            // Modelo de retorno.
            ModeloRetorno retorno = null;
            // BLL.
            ProjetoBLL bll = null;
            // Tentativa.
            try
            {
                // Instanciando bll.
                bll = new ProjetoBLL();
                // Inserindo projeto.
                if (bll.AtualizarProjeto(projetoCompleto))
                    retorno = new ModeloRetorno()
                    {
                        codigo = 0,
                        mensagem = $"Projeto '{projetoCompleto.projeto.NOMPRJ}' atualizado com sucesso."
                    };
                else
                    retorno = new ModeloRetorno()
                    {
                        codigo = 2,
                        mensagem = "Erro ao atualizar projeto. (Algumas tabelas não foram afetadas)"
                    };
            }
            catch (Exception ex)
            {
                retorno = new ModeloRetorno()
                {
                    codigo = 1,
                    mensagem = $"Erro ao atualizar projeto: {ex.Message}"
                };
            }
            finally
            {
                // Limpando variável.
                bll = null;
            }
            // Retornando.
            return Ok(retorno);
        }
        #endregion

        #region TipoProjeto

        /// <summary>
        /// Obtem os tipos de projeto.
        /// </summary>
        /// <param name="codTip">Código do tipo de projeto.</param>
        /// <param name="nomTip">Nome do tipo de projeto.</param>
        /// <returns>Lista de Projetos.</returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        [HttpGet, Route("ObterTipos")]
        public IHttpActionResult ObterTipos(int codTip, string nomTip)
        {
            // Modelo de retorno.
            ModeloRetorno<List<TipoProjeto>> retorno = null;
            // BLL.
            ProjetoBLL bll = null;
            // Tentativa.
            try
            {
                // Instanciando bll.
                bll = new ProjetoBLL();
                // Obtendo tipos de projeto.
                retorno = new ModeloRetorno<List<TipoProjeto>>()
                {
                    codigo = 0,
                    objeto = bll.ObterTiposProjeto(codTip, nomTip)
                };
            }
            catch (Exception ex)
            {
                retorno = new ModeloRetorno<List<TipoProjeto>>()
                {
                    codigo = 1,
                    mensagem = $"Erro ao obter tipos de projeto: {ex.Message}"
                };
            }
            finally
            {
                // Limpando variável.
                bll = null;
            }
            // Retornando.
            return Ok(retorno);
        }

        /// <summary>
        /// Insere um novo tipo de projeto.
        /// </summary>
        /// <param name="tipoProjeto">Objeto do tipo de projeto.</param>
        /// <returns>Modelo Retorno.</returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        [HttpPost, Route("InserirTipo")]
        public IHttpActionResult InserirTipo(TipoProjeto tipoProjeto)
        {
            // Modelo de retorno.
            ModeloRetorno retorno = null;
            // BLL.
            ProjetoBLL bll = null;
            // Tentativa.
            try
            {
                // Instanciando bll.
                bll = new ProjetoBLL();
                // Inserindo tipo de projeto.
                if (bll.InserirTipoProjeto(tipoProjeto))
                    retorno = new ModeloRetorno()
                    {
                        codigo = 0,
                        mensagem = $"Tipo de Projeto '{tipoProjeto.NOMTIP}' inserido com sucesso."
                    };
                else
                    retorno = new ModeloRetorno()
                    {
                        codigo = 2,
                        mensagem = "Erro ao inserir tipo de projeto. (A tabela não foi afetada)"
                    };
            }
            catch (Exception ex)
            {
                retorno = new ModeloRetorno()
                {
                    codigo = 1,
                    mensagem = $"Erro ao inserir tipo de projeto: {ex.Message}"
                };
            }
            finally
            {
                // Limpando variável.
                bll = null;
            }
            // Retornando.
            return Ok(retorno);
        }

        /// <summary>
        /// Atualiza os dados do tipo de projeto.
        /// </summary>
        /// <param name="tipoProjeto">Objeto do tipo de projeto.</param>
        /// <returns>Modelo Retorno.</returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        [HttpPost, Route("AtualizarTipo")]
        public IHttpActionResult AtualizarTipo(TipoProjeto tipoProjeto)
        {
            // Modelo de retorno.
            ModeloRetorno retorno = null;
            // BLL.
            ProjetoBLL bll = null;
            // Tentativa.
            try
            {
                // Instanciando bll.
                bll = new ProjetoBLL();
                // Atualiza o tipo de projeto.
                if (bll.AtualizarTipoProjeto(tipoProjeto))
                    retorno = new ModeloRetorno()
                    {
                        codigo = 0,
                        mensagem = $"Tipo de Projeto '{tipoProjeto.NOMTIP}' atualizado com sucesso."
                    };
                else
                    retorno = new ModeloRetorno()
                    {
                        codigo = 2,
                        mensagem = "Erro ao atualizar tipo de projeto. (A tabela não foi afetada)"
                    };
            }
            catch (Exception ex)
            {
                retorno = new ModeloRetorno()
                {
                    codigo = 1,
                    mensagem = $"Erro ao atualizar tipo de projeto: {ex.Message}"
                };
            }
            finally
            {
                // Limpando variável.
                bll = null;
            }
            // Retornando.
            return Ok(retorno);
        }

        /// <summary>
        /// Atualiza os dados do tipo de projeto.
        /// </summary>
        /// <param name="tipoProjeto">Objeto do tipo de projeto.</param>
        /// <returns>Modelo Retorno.</returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        [HttpPost, Route("RemoverTipo")]
        public IHttpActionResult RemoverTipo(TipoProjeto tipoProjeto)
        {
            // Modelo de retorno.
            ModeloRetorno retorno = null;
            // BLL.
            ProjetoBLL bll = null;
            // Tentativa.
            try
            {
                // Instanciando bll.
                bll = new ProjetoBLL();
                // Remove o tipo de projeto.
                if (bll.RemoverTipoProjeto(tipoProjeto))
                    retorno = new ModeloRetorno()
                    {
                        codigo = 0,
                        mensagem = $"Tipo de Projeto '{tipoProjeto.NOMTIP}' removido com sucesso."
                    };
                else
                    retorno = new ModeloRetorno()
                    {
                        codigo = 2,
                        mensagem = "Erro ao remover tipo de projeto. (A tabela não foi afetada)"
                    };
            }
            catch (Exception ex)
            {
                retorno = new ModeloRetorno()
                {
                    codigo = 1,
                    mensagem = $"Erro ao remover tipo de projeto: {ex.Message}"
                };
            }
            finally
            {
                // Limpando variável.
                bll = null;
            }
            // Retornando.
            return Ok(retorno);
        }
        #endregion
    }
}