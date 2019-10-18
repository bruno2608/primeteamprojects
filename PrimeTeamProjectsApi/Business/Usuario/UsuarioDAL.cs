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
    public class UsuarioDAL : DAL
    {
        /// <summary>
        /// Obtem os usuários cadastrados.
        /// </summary>
        /// <param name="codUsr">Código do usuário.</param>
        /// <param name="usrId">ID do usuário.</param>
        /// <param name="usrPsw">Senha do usuário.</param>
        /// <param name="nomUsr">Nome do usuário.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public List<Usuario> ObterUsuario(int codUsr, string usrId, string usrPsw, string nomUsr)
        {
            // DALSQL.
            UsuarioDALSQL dalSql = null;
            // Comando.
            SqlCommand command = null;
            // Tentativa.
            try
            {
                // Instanciando DALSQL.
                dalSql = new UsuarioDALSQL();
                // Obtendo comando.
                command = this.GetCommand(dalSql.ObterUsuario(codUsr, usrId, usrPsw, nomUsr));
                // Adicionando Parâmetros.
                if (codUsr != -1)
                    command.AddParameter("@CODUSR", codUsr);
                if (usrId != null && usrId.Trim().Length > 0)
                    command.AddParameter("@USRID", usrId);
                if (usrPsw != null && usrPsw.Trim().Length > 0)
                    command.AddParameter("@USRPSW", usrPsw);
                if (nomUsr != null && nomUsr.Trim().Length > 0)
                    command.AddParameter("@NOMUSR", nomUsr);
                // Retornando lista.
                return command.ExecuteList<Usuario>();
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
        /// Verifica se o usuário está relacionado com alguma atividade.
        /// </summary>
        /// <param name="codUsr">Código do usuário.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public int VerificarAtividades(int codUsr)
        {
            // DALSQL.
            UsuarioDALSQL dalSql = null;
            // Comando.
            SqlCommand command = null;
            // Retorno scalar.
            object scalar = null;
            // Tentativa.
            try
            {
                // Instanciando DALSQL.
                dalSql = new UsuarioDALSQL();
                // Obtendo comando.
                command = this.GetCommand(dalSql.VerificarAtividades());
                // Adicionando Parâmetro.
                command.AddParameter("@CODUSR", codUsr);
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
        /// Efetua a inserção de um novo usuário.
        /// </summary>
        /// <param name="usuario">Objeto do usuário.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public int InserirUsuario(Usuario usuario)
        {
            // DALSQL.
            UsuarioDALSQL dalSql = null;
            // Comando.
            SqlCommand command = null;
            // Tentativa.
            try
            {
                // Instanciando DALSQL.
                dalSql = new UsuarioDALSQL();
                // Obtendo comando.
                command = this.GetCommand(dalSql.InserirUsuario());
                // Adicionando Parâmetros.
                command.AddParameter("@USRID", usuario.USRID);
                command.AddParameter("@USRPSW", usuario.USRPSW);
                command.AddParameter("@NOMUSR", usuario.NOMUSR);
                command.AddParameter("@USRMAIL", usuario.USRMAIL);
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
        /// Atualiza os dados do usuário.
        /// </summary>
        /// <param name="usuario">Objeto do usuário.</param>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public int AtualizarUsuario(Usuario usuario)
        {
            // DALSQL.
            UsuarioDALSQL dalSql = null;
            // Comando.
            SqlCommand command = null;
            // Tentativa.
            try
            {
                // Instanciando DALSQL.
                dalSql = new UsuarioDALSQL();
                // Obtendo comando.
                command = this.GetCommand(dalSql.AtualizarUsuario());
                // Adicionando Parâmetros.
                command.AddParameter("@USRID", usuario.USRID);
                command.AddParameter("@USRPSW", usuario.USRPSW);
                command.AddParameter("@NOMUSR", usuario.NOMUSR);
                command.AddParameter("@USRMAIL", usuario.USRMAIL);
                command.AddParameter("@CODUSR", usuario.CODUSR);
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
        /// Desativa o usuário.
        /// </summary>
        /// <param name="usuario">Objeto do usuário.</param>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public int DesativarUsuario(Usuario usuario)
        {
            // DALSQL.
            UsuarioDALSQL dalSql = null;
            // Comando.
            SqlCommand command = null;
            // Tentativa.
            try
            {
                // Instanciando DALSQL.
                dalSql = new UsuarioDALSQL();
                // Obtendo comando.
                command = this.GetCommand(dalSql.DesativarUsuario());
                // Adicionando Parâmetro.
                command.AddParameter("@CODUSR", usuario.CODUSR);
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
        /// Ativa o usuário.
        /// </summary>
        /// <param name="usuario">Objeto do usuário.</param>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public int AtivarUsuario(Usuario usuario)
        {
            // DALSQL.
            UsuarioDALSQL dalSql = null;
            // Comando.
            SqlCommand command = null;
            // Tentativa.
            try
            {
                // Instanciando DALSQL.
                dalSql = new UsuarioDALSQL();
                // Obtendo comando.
                command = this.GetCommand(dalSql.AtivarUsuario());
                // Adicionando Parâmetro.
                command.AddParameter("@CODUSR", usuario.CODUSR);
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