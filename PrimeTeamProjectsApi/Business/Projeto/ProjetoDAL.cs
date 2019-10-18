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
    public class ProjetoDAL : DAL
    {
        #region Projeto

        /// <summary>
        /// Obtem os projetos cadastrados no sistema.
        /// </summary>
        /// <param name="codPrj">Código do projeto.</param>
        /// <param name="nomPrj">Nome do projeto.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public List<Projeto> ObterProjetos(int codPrj, string nomPrj)
        {
            // DALSQL.
            ProjetoDALSQL dalSql = null;
            // Comando.
            SqlCommand command = null;
            // Tentativa.
            try
            {
                // Instanciando DALSQL.
                dalSql = new ProjetoDALSQL();
                // Obtendo comando.
                command = this.GetCommand(dalSql.ObterProjetos(codPrj, nomPrj));
                // Adicionando Parâmetros.
                if (codPrj != -1)
                    command.AddParameter("@CODPRJ", codPrj);
                if (nomPrj != null && nomPrj.Trim().Length > 0)
                    command.AddParameter("@NOMPRJ", nomPrj);
                // Retornando lista.
                return command.ExecuteList<Projeto>();
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
        /// Obtem o próximo código da tabela de projeto.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public int ObterProximoProjeto()
        {
            // DALSQL.
            ProjetoDALSQL dalSql = null;
            // Comando.
            SqlCommand command = null;
            // Retorno scalar.
            object scalar = null;
            // Tentativa.
            try
            {
                // Instanciando DALSQL.
                dalSql = new ProjetoDALSQL();
                // Obtendo comando.
                command = this.GetCommand(dalSql.ObterProximoProjeto());
                // Obtendo resultado.
                scalar = command.ExecuteScalar();
                // Verificando resultado.
                if (scalar != null && int.TryParse(scalar.ToString(), out int prxcodprj))
                {
                    return prxcodprj;
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
        /// Inclui um novo projeto no sistema.
        /// </summary>
        /// <param name="projeto">Objeto do projeto.</param>
        /// <returns></returns>
        public int InserirProjeto(Projeto projeto)
        {
            // DALSQL.
            ProjetoDALSQL dalSql = null;
            // Comando.
            SqlCommand command = null;
            // Tentativa.
            try
            {
                // Instanciando DALSQL.
                dalSql = new ProjetoDALSQL();
                // Obtendo comando.
                command = this.GetCommand(dalSql.InserirProjeto());
                // Adicionando Parâmetros.
                command.AddParameter("@NOMPRJ", projeto.NOMPRJ);
                command.AddParameter("@DESPRJ", projeto.DESPRJ);
                if (projeto.DATINISTR != null && projeto.DATINISTR.Trim().Length > 0)
                    command.AddParameter("@DATINIPRJ", Convert.ToDateTime(projeto.DATINISTR));
                else
                    command.AddParameter("@DATINIPRJ", DBNull.Value);
                if (projeto.DATFIMSTR != null && projeto.DATFIMSTR.Trim().Length > 0)
                    command.AddParameter("@DATFIMPRJ", Convert.ToDateTime(projeto.DATFIMSTR));
                else
                    command.AddParameter("@DATFIMPRJ", DBNull.Value);
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
        ///  Atualiza os dados do projeto.
        /// </summary>
        /// <param name="projeto">Objeto do projeto.</param>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public int AtualizarProjeto(Projeto projeto)
        {
            // DALSQL.
            ProjetoDALSQL dalSql = null;
            // Comando.
            SqlCommand command = null;
            // Tentativa.
            try
            {
                // Instanciando DALSQL.
                dalSql = new ProjetoDALSQL();
                // Obtendo comando.
                command = this.GetCommand(dalSql.AtualizarProjeto());
                // Adicionando Parâmetros.
                command.AddParameter("@NOMPRJ", projeto.NOMPRJ);
                command.AddParameter("@DESPRJ", projeto.DESPRJ);
                if (projeto.DATINISTR != null && projeto.DATINISTR.Trim().Length > 0)
                    command.AddParameter("@DATINIPRJ", Convert.ToDateTime(projeto.DATINISTR));
                else
                    command.AddParameter("@DATINIPRJ", DBNull.Value);
                if (projeto.DATFIMSTR != null && projeto.DATFIMSTR.Trim().Length > 0)
                    command.AddParameter("@DATFIMPRJ", Convert.ToDateTime(projeto.DATFIMSTR));
                else
                    command.AddParameter("@DATFIMPRJ", DBNull.Value);
                command.AddParameter("@CODPRJ", projeto.CODPRJ);
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
        #endregion

        #region TipoProjeto

        /// <summary>
        /// Obtem os tipos de projeto.
        /// </summary>
        /// <param name="codTip">Código do tipo de projeto.</param>
        /// <param name="nomTip">Nome do tipo de projeto.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public List<TipoProjeto> ObterTiposProjeto(int codTip, string nomTip)
        {
            // DALSQL.
            ProjetoDALSQL dalSql = null;
            // Comando.
            SqlCommand command = null;
            // Tentativa.
            try
            {
                // Instanciando DALSQL.
                dalSql = new ProjetoDALSQL();
                // Obtendo comando.
                command = this.GetCommand(dalSql.ObterTiposProjeto(codTip, nomTip));
                // Adicionando Parâmetros.
                if (codTip != -1)
                    command.AddParameter("@CODTIP", codTip);
                if (nomTip != null && nomTip.Trim().Length > 0)
                    command.AddParameter("@NOMTIP", nomTip);
                // Retornando lista.
                return command.ExecuteList<TipoProjeto>();
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
        /// Verifica se o tipo de projeto já está sendo utilizado.
        /// </summary>
        /// <param name="codTip">Código do tipo de projeto.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public int VerificarProjetos(int codTip)
        {
            // DALSQL.
            ProjetoDALSQL dalSql = null;
            // Comando.
            SqlCommand command = null;
            // Retorno scalar.
            object scalar = null;
            // Tentativa.
            try
            {
                // Instanciando DALSQL.
                dalSql = new ProjetoDALSQL();
                // Obtendo comando.
                command = this.GetCommand(dalSql.VerificarProjetos());
                // Adicionando Parâmetro.
                command.AddParameter("@CODTIP", codTip);
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
        /// Insere um novo tipo de projeto.
        /// </summary>
        /// <param name="tipoProjeto">Objeto do tipo de projeto.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public int InserirTipoProjeto(TipoProjeto tipoProjeto)
        {
            // DALSQL.
            ProjetoDALSQL dalSql = null;
            // Comando.
            SqlCommand command = null;
            // Tentativa.
            try
            {
                // Instanciando DALSQL.
                dalSql = new ProjetoDALSQL();
                // Obtendo comando.
                command = this.GetCommand(dalSql.InserirTipoProjeto());
                // Adicionando Parâmetros.
                command.AddParameter("@NOMTIP", tipoProjeto.NOMTIP);
                command.AddParameter("@DESTIP", tipoProjeto.DESTIP);
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
        /// Atualiza os dados do tipo de projeto.
        /// </summary>
        /// <param name="tipoProjeto">Objeto do tipo de projeto.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public int AtualizarTipoProjeto(TipoProjeto tipoProjeto)
        {
            // DALSQL.
            ProjetoDALSQL dalSql = null;
            // Comando.
            SqlCommand command = null;
            // Tentativa.
            try
            {
                // Instanciando DALSQL.
                dalSql = new ProjetoDALSQL();
                // Obtendo comando.
                command = this.GetCommand(dalSql.AtualizarTipoProjeto());
                // Adicionando Parâmetros.
                command.AddParameter("@NOMTIP", tipoProjeto.NOMTIP);
                command.AddParameter("@DESTIP", tipoProjeto.DESTIP);
                command.AddParameter("@CODTIP", tipoProjeto.CODTIP);
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
        /// Deleta o tipo do projeto.
        /// </summary>
        /// <param name="codTip">Código do tipo de projeto.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public int RemoverTipoProjeto(int codTip)
        {
            // DALSQL.
            ProjetoDALSQL dalSql = null;
            // Comando.
            SqlCommand command = null;
            // Tentativa.
            try
            {
                // Instanciando DALSQL.
                dalSql = new ProjetoDALSQL();
                // Obtendo comando.
                command = this.GetCommand(dalSql.RemoverTipoProjeto());
                // Adicionando Parâmetro.
                command.AddParameter("@CODTIP", codTip);
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
        #endregion

        #region Projeto x Tipo
        /// <summary>
        /// Obtem relação entre um projeto e um tipo.
        /// </summary>
        /// <param name="codPrj">Código do projeto.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public List<TipoProjeto> ObterRelacaoPrjTip(int codPrj)
        {
            // DALSQL.
            ProjetoDALSQL dalSql = null;
            // Comando.
            SqlCommand command = null;
            // Tentativa.
            try
            {
                // Instanciando DALSQL.
                dalSql = new ProjetoDALSQL();
                // Obtendo comando.
                command = this.GetCommand(dalSql.ObterRelacaoPrjTip());
                // Adicionando Parâmetro.
                command.AddParameter("@CODPRJ", codPrj);
                // Retornando lista.
                return command.ExecuteList<TipoProjeto>();
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
        /// Insere uma relação entre um projeto e um tipo.
        /// </summary>
        /// <param name="codPrj">Código do projeto.</param>
        /// <param name="codTip">Código do tipo de projeto.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public int InserirRelacaoPrjTip(int codPrj, int codTip)
        {
            // DALSQL.
            ProjetoDALSQL dalSql = null;
            // Comando.
            SqlCommand command = null;
            // Tentativa.
            try
            {
                // Instanciando DALSQL.
                dalSql = new ProjetoDALSQL();
                // Obtendo comando.
                command = this.GetCommand(dalSql.InserirRelacaoPrjTip());
                // Adicionando Parâmetros.
                command.AddParameter("@CODPRJ", codPrj);
                command.AddParameter("@CODTIP", codTip);
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
        /// Delete a relação entre um projeto e um tipo.
        /// </summary>
        /// <param name="codPrj">Código do projeto.</param>
        /// <param name="codTip">Código do tipo de projeto.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public int RemoverRelacaoPrjTip(int codPrj, int codTip)
        {
            // DALSQL.
            ProjetoDALSQL dalSql = null;
            // Comando.
            SqlCommand command = null;
            // Tentativa.
            try
            {
                // Instanciando DALSQL.
                dalSql = new ProjetoDALSQL();
                // Obtendo comando.
                command = this.GetCommand(dalSql.RemoverRelacaoPrjTip());
                // Adicionando Parâmetros.
                command.AddParameter("@CODPRJ", codPrj);
                command.AddParameter("@CODTIP", codTip);
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
        #endregion

        #region Atividade x Usuario x Projeto
        /// <summary>
        /// QUERY: Obtem relação entre atividade x usuário x projeto.
        /// </summary>
        /// <param name="codPrj">Código do projeto.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public List<RelacaoAUP> ObterRelacaoAUP(int codPrj)
        {
            // DALSQL.
            ProjetoDALSQL dalSql = null;
            // Comando.
            SqlCommand command = null;
            // Tentativa.
            try
            {
                // Instanciando DALSQL.
                dalSql = new ProjetoDALSQL();
                // Obtendo comando.
                command = this.GetCommand(dalSql.ObterRelacaoAUP());
                // Adicionando Parâmetro.
                command.AddParameter("@CODPRJ", codPrj);
                // Retornando lista.
                return command.ExecuteList<RelacaoAUP>();
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
        /// Insere uma nova relação entre atividade x usuário x projeto.
        /// </summary>
        /// <param name="codAtv">Código da atividade.</param>
        /// <param name="codPrj">Código do projeto.</param>
        /// <param name="codUsr">Código do usuário.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public int InserirRelacaoAUP(int codAtv, int codPrj, int codUsr)
        {
            // DALSQL.
            ProjetoDALSQL dalSql = null;
            // Comando.
            SqlCommand command = null;
            // Tentativa.
            try
            {
                // Instanciando DALSQL.
                dalSql = new ProjetoDALSQL();
                // Obtendo comando.
                command = this.GetCommand(dalSql.InserirRelacaoAUP());
                // Adicionando Parâmetros.
                command.AddParameter("@CODATV", codAtv);
                command.AddParameter("@CODPRJ", codPrj);
                command.AddParameter("@CODUSR", codUsr);
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
        /// Remove a relação entre atividade x usuário x projeto.
        /// </summary>
        /// <param name="codAtv">Código da atividade.</param>
        /// <param name="codPrj">Código do projeto.</param>
        /// <param name="codUsr">Código do usuário.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public int RemoverRelacaoAUP(int codAtv, int codPrj, int codUsr)
        {
            // DALSQL.
            ProjetoDALSQL dalSql = null;
            // Comando.
            SqlCommand command = null;
            // Tentativa.
            try
            {
                // Instanciando DALSQL.
                dalSql = new ProjetoDALSQL();
                // Obtendo comando.
                command = this.GetCommand(dalSql.RemoverRelacaoAUP());
                // Adicionando Parâmetros.
                command.AddParameter("@CODATV", codAtv);
                command.AddParameter("@CODPRJ", codPrj);
                command.AddParameter("@CODUSR", codUsr);
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
        #endregion
    }
}