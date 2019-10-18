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
    /// Controller da página de cadastro de atividades.
    /// </summary>
    [RoutePrefix("api/Atividade")]
    public class AtividadeController : PrimeTeamController
    {
        /// <summary>
        /// Obtem as atividades cadastradas no sistema.
        /// </summary>
        /// <param name="codAtv">Código da atividade.</param>
        /// <param name="nomAtv">Nome da atividade.</param>
        /// <returns>Lista de Atividades.</returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        [HttpGet, Route("Obter")]
        public IHttpActionResult Obter(int codAtv, string nomAtv)
        {
            // Modelo de retorno.
            ModeloRetorno<List<Atividade>> retorno = null;
            // BLL.
            AtividadeBLL bll = null;
            // Tentativa.
            try
            {
                // Instanciando bll.
                bll = new AtividadeBLL();
                // Obtendo atividades.
                retorno = new ModeloRetorno<List<Atividade>>()
                {
                    codigo = 0,
                    objeto = bll.ObterAtividades(codAtv, nomAtv)
                };
            }
            catch (Exception ex)
            {
                retorno = new ModeloRetorno<List<Atividade>>()
                {
                    codigo = 1,
                    mensagem = $"Erro ao obter atividades: {ex.Message}"
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
        /// Insere uma nova atividade.
        /// </summary>
        /// <param name="atividade">Dados da atividades.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        [HttpPost, Route("Inserir")]
        public IHttpActionResult Inserir(Atividade atividade)
        {
            // Modelo de retorno.
            ModeloRetorno retorno = null;
            // BLL.
            AtividadeBLL bll = null;
            // Tentativa.
            try
            {
                // Instanciando bll.
                bll = new AtividadeBLL();
                // Inserindo atividade.
                if (bll.IntserirAtividade(atividade))
                    retorno = new ModeloRetorno()
                    {
                        codigo = 0,
                        mensagem = $"Atividade '{atividade.NOMATV}' inserida com sucesso."
                    };
                else
                    retorno = new ModeloRetorno()
                    {
                        codigo = 2,
                        mensagem = "Erro ao inserir atividade. (A tabela não foi afetada)"
                    };
            }
            catch (Exception ex)
            {
                retorno = new ModeloRetorno()
                {
                    codigo = 1,
                    mensagem = $"Erro ao inserir atividade: {ex.Message}"
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
        /// Atualiza os dados da atividade.
        /// </summary>
        /// <param name="atividade">Dados da atividades.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        [HttpPost, Route("Atualizar")]
        public IHttpActionResult Atualizar(Atividade atividade)
        {
            // Modelo de retorno.
            ModeloRetorno retorno = null;
            // BLL.
            AtividadeBLL bll = null;
            // Tentativa.
            try
            {
                // Instanciando bll.
                bll = new AtividadeBLL();
                // Atualizando atividade.
                if (bll.AtualizarAtividade(atividade))
                    retorno = new ModeloRetorno()
                    {
                        codigo = 0,
                        mensagem = $"Atividade '{atividade.NOMATV}' atualizada com sucesso."
                    };
                else
                    retorno = new ModeloRetorno()
                    {
                        codigo = 2,
                        mensagem = "Erro ao atualizar atividade. (A tabela não foi afetada)"
                    };
            }
            catch (Exception ex)
            {
                retorno = new ModeloRetorno()
                {
                    codigo = 1,
                    mensagem = $"Erro ao atualizar atividade: {ex.Message}"
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
        /// Deleta a atividade.
        /// </summary>
        /// <param name="atividade">Dados da atividades.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        [HttpPost, Route("Remover")]
        public IHttpActionResult Remover(Atividade atividade)
        {
            // Modelo de retorno.
            ModeloRetorno retorno = null;
            // BLL.
            AtividadeBLL bll = null;
            // Tentativa.
            try
            {
                // Instanciando bll.
                bll = new AtividadeBLL();
                // Atualizando atividade.
                if (bll.RemoverAtividade(atividade))
                    retorno = new ModeloRetorno()
                    {
                        codigo = 0,
                        mensagem = $"Atividade '{atividade.NOMATV}' removida com sucesso."
                    };
                else
                    retorno = new ModeloRetorno()
                    {
                        codigo = 2,
                        mensagem = "Erro ao remover atividade. (A tabela não foi afetada)"
                    };
            }
            catch (Exception ex)
            {
                retorno = new ModeloRetorno()
                {
                    codigo = 1,
                    mensagem = $"Erro ao remover atividade: {ex.Message}"
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


    }
}