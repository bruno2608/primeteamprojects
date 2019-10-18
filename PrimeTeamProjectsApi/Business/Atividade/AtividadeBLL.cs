using System;
using System.Linq;
using System.Transactions;
using System.Collections.Generic;
using PrimeTeamProjectsApi.Models;

namespace PrimeTeamProjectsApi.Business
{
    /// <summary>
    /// Classe de negócio.
    /// </summary>
    public class AtividadeBLL
    {

        /// <summary>
        /// Obtem as atividades cadastradas no sistema.
        /// </summary>
        /// <param name="codAtv">Código da atividade.</param>
        /// <param name="nomAtv">Nome da atividade.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public List<Atividade> ObterAtividades(int codAtv, string nomAtv)
        {
            // DAL.
            AtividadeDAL dal = null;
            // Tentativa.
            try
            {
                // Instanciando dal.
                dal = new AtividadeDAL();
                // Retornando consulta.
                return dal.ObterAtividades(codAtv, nomAtv);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Limpando variável.
                dal = null;
            }
        }

        /// <summary>
        /// Insere uma nova atividade.
        /// </summary>
        /// <param name="atividade">Objeto da atividade.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public bool IntserirAtividade(Atividade atividade)
        {
            // DAL.
            AtividadeDAL dal = null;
            // Tentativa.
            try
            {
                // Instanciando dal.
                dal = new AtividadeDAL();
                // Executando.
                return dal.IntserirAtividade(atividade) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Limpando variável.
                dal = null;
            }
        }

        /// <summary>
        /// Atualiza os dados da atividade.
        /// </summary>
        /// <param name="atividade">Objeto da atividade.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public bool AtualizarAtividade(Atividade atividade)
        {
            // DAL.
            AtividadeDAL dal = null;
            // Tentativa.
            try
            {
                // Instanciando dal.
                dal = new AtividadeDAL();
                // Executando.
                return dal.AtualizarAtividade(atividade) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Limpando variável.
                dal = null;
            }
        }

        /// <summary>
        /// Atualiza as horas trabalhadas na atividade.
        /// </summary>
        /// <param name="codAtv">Código da atividade.</param>
        /// <param name="tmpEstAtv">Tempo trabalhado na atividade.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public bool AtualizarHoras(int codAtv, long tmpEstAtv)
        {
            // DAL.
            AtividadeDAL dal = null;
            // Tentativa.
            try
            {
                // Instanciando dal.
                dal = new AtividadeDAL();
                // Executando.
                return dal.AtualizarHoras(codAtv, tmpEstAtv) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Limpando variável.
                dal = null;
            }
        }

        /// <summary>
        /// Deleta a atividade.
        /// </summary>
        /// <param name="atividade">Objeto da atividade.</param>
        /// <returns></returns>
        public bool RemoverAtividade(Atividade atividade)
        {
            // Criando um bloco de transação.
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                // DAL.
                AtividadeDAL dal = null;
                // Tentativa.
                try
                {
                    // Instanciando dal.
                    dal = new AtividadeDAL();
                    // Obtendo projetos da atividade.
                    int projetos = dal.VerificarProjetos(atividade.CODATV);
                    // Verificando se a atividade já está cadastrada em algum projeto.
                    if (projetos > 0)
                        throw new Exception($"Não foi possível remover a atividade '{atividade.NOMATV}' pois, a mesma já está cadastrada em {projetos} projeto(s).");
                    // Executando.
                    int rmAtv = dal.RemoverAtividade(atividade.CODATV);
                    // Completando o scopo (commit).
                    scope.Complete();
                    // Retornando.
                    return rmAtv > 0;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    // Finalizando transação.
                    scope.Dispose();
                    // Limpando variável.
                    dal = null;
                }
            }
        }
    }
}