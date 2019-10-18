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
    public class UsuarioBLL
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
            // DAL.
            UsuarioDAL dal = null;
            // Tentativa.
            try
            {
                // Instanciando dal.
                dal = new UsuarioDAL();
                // Retornando consulta.
                return dal.ObterUsuario(codUsr, usrId, usrPsw, nomUsr);
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
        /// Efetua a inserção de um novo usuário.
        /// </summary>
        /// <param name="usuario">Modelo do usuário.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public bool InserirUsuario(Usuario usuario)
        {
            // DAL.
            UsuarioDAL dal = null;
            // Tentativa.
            try
            {
                // Instanciando dal.
                dal = new UsuarioDAL();
                // Executando.
                return dal.InserirUsuario(usuario) > 0;
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
        /// Atualiza os dados do usuário.
        /// </summary>
        /// <param name="usuario">Modelo do usuário.</param>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public bool AtualizarUsuario(Usuario usuario)
        {
            // DAL.
            UsuarioDAL dal = null;
            // Tentativa.
            try
            {
                // Instanciando dal.
                dal = new UsuarioDAL();
                // Executando.
                return dal.AtualizarUsuario(usuario) > 0;
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
        /// Desativa o usuário.
        /// </summary>
        /// <param name="usuario">Modelo do usuário.</param>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public bool DesativarUsuario(Usuario usuario)
        {
            // Criando um bloco de transação.
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                // DAL.
                UsuarioDAL dal = null;
                // Tentativa.
                try
                {
                    // Instanciando dal.
                    dal = new UsuarioDAL();
                    // Obtendo atividades do usuário.
                    int atividades = dal.VerificarAtividades(usuario.CODUSR);
                    // Se o usuário possuí atividades impedir desativação do mesmo.
                    if (atividades > 0)
                        throw new Exception($"Não foi possível desativar o usuário '{usuario.NOMUSR}' pois, o mesmo já está cadastrado em {atividades} atividade(s).");
                    // Executando.
                    int dstUsr = dal.DesativarUsuario(usuario);
                    // Completando o scopo (commit).
                    scope.Complete();
                    // Retornando.
                    return dstUsr > 0;
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

        /// <summary>
        /// Ativa o usuário.
        /// </summary>
        /// <param name="usuario">Modelo do usuário.</param>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public bool AtivarUsuario(Usuario usuario)
        {
            // DAL.
            UsuarioDAL dal = null;
            // Tentativa.
            try
            {
                // Instanciando dal.
                dal = new UsuarioDAL();
                // Retornando.
                return dal.AtivarUsuario(usuario) > 0;
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

    }
}