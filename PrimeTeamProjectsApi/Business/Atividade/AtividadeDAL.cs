using System;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using PrimeTeamProjectsApi.Models;

namespace PrimeTeamProjectsApi.Business
{
    /// <summary>
    /// Classe de conexão/execução.
    /// </summary>
    public class AtividadeDAL : DAL
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
            // DALSQL.
            AtividadeDALSQL dalSql = null;
            // Comando.
            SqlCommand command = null;
            // Tentativa.
            try
            {
                // Instanciando DALSQL.
                dalSql = new AtividadeDALSQL();
                // Obtendo comando.
                command = this.GetCommand(dalSql.ObterAtividades(codAtv, nomAtv));
                // Adicionando Parâmetros.
                if (codAtv != -1)
                    command.AddParameter("@CODATV", codAtv);
                if (nomAtv != null && nomAtv.Trim().Length > 0)
                    command.AddParameter("@NOMATV", nomAtv);
                // Retornando lista.
                return command.ExecuteList<Atividade>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Limpando variáveis.
                dalSql = null;
                if (command != null)
                {
                    
                    command.Dispose();
                    command = null;
                }
                // Limpando memória.
                GC.Collect();
            }
        }

        /// <summary>
        /// Verifica se a atividade está relacionada a algum projeto.
        /// </summary>
        /// <param name="codAtv">Código da atividade.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public int VerificarProjetos(int codAtv)
        {
            // DALSQL.
            AtividadeDALSQL dalSql = null;
            // Comando.
            SqlCommand command = null;
            // Retorno scalar.
            object scalar = null;
            // Tentativa.
            try
            {
                // Instanciando DALSQL.
                dalSql = new AtividadeDALSQL();
                // Obtendo comando.
                command = this.GetCommand(dalSql.VerificarProjetos());
                // Adicionando Parâmetro.
                command.AddParameter("@CODATV", codAtv);
                // Obtendo resultado.
                scalar = command.ExecuteScalar();
                // Verificando resultado.
                if (scalar != null && int.TryParse(scalar.ToString(), out int existe))
                {
                    return existe;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Limpando variáveis.
                dalSql = null;
                scalar = null;
                if (command != null)
                {
                    
                    command.Dispose();
                    command = null;
                }
                // Limpando memória.
                GC.Collect();
            }
        }

        /// <summary>
        /// Insere uma nova atividade.
        /// </summary>
        /// <param name="atividade">Objeto da atividade.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public int IntserirAtividade(Atividade atividade)
        {
            // DALSQL.
            AtividadeDALSQL dalSql = null;
            // Comando.
            SqlCommand command = null;
            // Tentativa.
            try
            {
                // Instanciando DALSQL.
                dalSql = new AtividadeDALSQL();
                // Obtendo comando.
                command = this.GetCommand(dalSql.InserirAtividade());
                // Adicionando Parâmetros.
                command.AddParameter("@NOMATV", atividade.NOMATV);
                command.AddParameter("@DESATV", atividade.DESATV);
                command.AddParameter("@TMPESTATV", atividade.TMPESTATV);
                command.AddParameter("@DATINIATV", atividade.DATINIATV);
                command.AddParameter("@DATFIMATV", atividade.DATFIMATV);
                // Executando comando.
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Limpando variáveis.
                dalSql = null;
                if (command != null)
                {
                    
                    command.Dispose();
                    command = null;
                }
                // Limpando memória.
                GC.Collect();
            }
        }

        /// <summary>
        /// Atualiza os dados da atividade.
        /// </summary>
        /// <param name="atividade">Objeto da atividade.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public int AtualizarAtividade(Atividade atividade)
        {
            // DALSQL.
            AtividadeDALSQL dalSql = null;
            // Comando.
            SqlCommand command = null;
            // Tentativa.
            try
            {
                // Instanciando DALSQL.
                dalSql = new AtividadeDALSQL();
                // Obtendo comando.
                command = this.GetCommand(dalSql.AtualizarAtividade());
                // Adicionando Parâmetros.
                command.AddParameter("@NOMATV", atividade.NOMATV);
                command.AddParameter("@DESATV", atividade.DESATV);
                command.AddParameter("@TMPESTATV", atividade.TMPESTATV);
                command.AddParameter("@DATINIATV", atividade.DATINIATV);
                command.AddParameter("@DATFIMATV", atividade.DATFIMATV);
                command.AddParameter("@CODATV", atividade.CODATV);
                // Executando comando.
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Limpando variáveis.
                dalSql = null;
                if (command != null)
                {
                    
                    command.Dispose();
                    command = null;
                }
                // Limpando memória.
                GC.Collect();
            }
        }

        /// <summary>
        /// Atualiza as horas trabalhadas na atividade.
        /// </summary>
        /// <param name="codAtv">Código da atividade.</param>
        /// <param name="tmpEstAtv">Tempo trabalhado na atividade.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public int AtualizarHoras(int codAtv, long tmpEstAtv)
        {
            // DALSQL.
            AtividadeDALSQL dalSql = null;
            // Comando.
            SqlCommand command = null;
            // Tentativa.
            try
            {
                // Instanciando DALSQL.
                dalSql = new AtividadeDALSQL();
                // Obtendo comando.
                command = this.GetCommand(dalSql.AtualizarHoras());
                // Adicionando Parâmetros.
                command.AddParameter("@TMPESTATV", tmpEstAtv);
                command.AddParameter("@CODATV", codAtv);
                // Executando comando.
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Limpando variáveis.
                dalSql = null;
                if (command != null)
                {
                    
                    command.Dispose();
                    command = null;
                }
                // Limpando memória.
                GC.Collect();
            }
        }

        /// <summary>
        /// Deleta a atividade.
        /// </summary>
        /// <param name="codAtv">Código da atividade.</param>
        /// <returns></returns>
        public int RemoverAtividade(int codAtv)
        {
            // DALSQL.
            AtividadeDALSQL dalSql = null;
            // Comando.
            SqlCommand command = null;
            // Tentativa.
            try
            {
                // Instanciando DALSQL.
                dalSql = new AtividadeDALSQL();
                // Obtendo comando.
                command = this.GetCommand(dalSql.RemoverAtividade());
                // Adicionando Parâmetro.
                command.AddParameter("@CODATV", codAtv);
                // Executando comando.
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Limpando variáveis.
                dalSql = null;
                if (command != null)
                {
                    
                    command.Dispose();
                    command = null;
                }
                // Limpando memória.
                GC.Collect();
            }
        }
    }
}
