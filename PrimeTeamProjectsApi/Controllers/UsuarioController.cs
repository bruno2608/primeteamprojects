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
    /// Controller da página de cadastro de usuários.
    /// </summary>
    [RoutePrefix("api/Usuario")]
    public class UsuarioController : PrimeTeamController
    {
        /// <summary>
        /// Obtem os usuários cadastrados.
        /// </summary>
        /// <param name="codUsr">Código do usuário.</param>
        /// <param name="usrId">ID do usuário.</param>
        /// <param name="nomUsr">Nome do usuário.</param>
        /// <returns>Lista de usuários.</returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        [HttpGet, Route("Obter")]
        public IHttpActionResult Obter(int codUsr, string usrId, string nomUsr)
        {
            // Modelo de retorno.
            ModeloRetorno<List<Usuario>> retorno = null;
            // BLL.
            UsuarioBLL bll = null;
            // Tentativa.
            try
            {
                // Instanciando bll.
                bll = new UsuarioBLL();
                // Obtendo usuarios.
                retorno = new ModeloRetorno<List<Usuario>>()
                {
                    codigo = 0,
                    objeto = bll.ObterUsuario(codUsr, usrId, null, nomUsr)
                };
            }
            catch (Exception ex)
            {
                retorno = new ModeloRetorno<List<Usuario>>()
                {
                    codigo = 1,
                    mensagem = $"Erro ao obter usuário: {ex.Message}"
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
        /// Efetua a inserção de um novo usuário.
        /// </summary>
        /// <param name="usuario">Modelo do usuário.</param>
        /// <returns>Modelo Retorno.</returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        [HttpPost, Route("Inserir")]
        public IHttpActionResult Inserir(Usuario usuario)
        {
            // Modelo de retorno.
            ModeloRetorno retorno = null;
            // BLL.
            UsuarioBLL bll = null;
            // Tentativa.
            try
            {
                // Instanciando bll.
                bll = new UsuarioBLL();
                // Inserindo usuário.
                if (bll.InserirUsuario(usuario))
                    retorno = new ModeloRetorno()
                    {
                        codigo = 0,
                        mensagem = $"Usuário '{usuario.NOMUSR}' inserido com sucesso."
                    };
                else
                    retorno = new ModeloRetorno()
                    {
                        codigo = 2,
                        mensagem = "Erro ao inserir usuário. (A tabela não foi afetada)"
                    };
            }
            catch (Exception ex)
            {
                retorno = new ModeloRetorno()
                {
                    codigo = 1,
                    mensagem = $"Erro ao inserir usuário: {ex.Message}"
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
        /// Atualiza os dados do usuário.
        /// </summary>
        /// <param name="usuario">Modelo do usuário.</param>
        /// <returns>Modelo Retorno.</returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        [HttpPost, Route("Atualizar")]
        public IHttpActionResult Atualizar(Usuario usuario)
        {
            // Modelo de retorno.
            ModeloRetorno retorno = null;
            // BLL.
            UsuarioBLL bll = null;
            // Tentativa.
            try
            {
                // Instanciando bll.
                bll = new UsuarioBLL();
                // Atualizando usuário.
                if (bll.AtualizarUsuario(usuario))
                    retorno = new ModeloRetorno()
                    {
                        codigo = 0,
                        mensagem = $"Usuário '{usuario.NOMUSR}' atualizado com sucesso."
                    };
                else
                    retorno = new ModeloRetorno()
                    {
                        codigo = 2,
                        mensagem = "Erro ao atualizar usuário. (A tabela não foi afetada)"
                    };
            }
            catch (Exception ex)
            {
                retorno = new ModeloRetorno()
                {
                    codigo = 1,
                    mensagem = $"Erro ao atualizar usuário: {ex.Message}"
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
        /// Desativa o usuário.
        /// </summary>
        /// <param name="usuario">Modelo do usuário.</param>
        /// <returns>Modelo Retorno.</returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        [HttpPost, Route("Desativar")]
        public IHttpActionResult Desativar(Usuario usuario)
        {
            // Modelo de retorno.
            ModeloRetorno retorno = null;
            // BLL.
            UsuarioBLL bll = null;
            // Tentativa.
            try
            {
                // Instanciando bll.
                bll = new UsuarioBLL();
                // Desativando usuário.
                if (bll.DesativarUsuario(usuario))
                    retorno = new ModeloRetorno()
                    {
                        codigo = 0,
                        mensagem = $"Usuário '{usuario.NOMUSR}' desativado com sucesso."
                    };
                else
                    retorno = new ModeloRetorno()
                    {
                        codigo = 2,
                        mensagem = "Erro ao desativar usuário. (A tabela não foi afetada)"
                    };
            }
            catch (Exception ex)
            {
                retorno = new ModeloRetorno()
                {
                    codigo = 1,
                    mensagem = $"Erro ao desativar usuário: {ex.Message}"
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
        /// Ativa o usuário.
        /// </summary>
        /// <param name="usuario">Modelo do usuário.</param>
        /// <returns>Modelo Retorno.</returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        [HttpPost, Route("Ativar")]
        public IHttpActionResult Ativar(Usuario usuario)
        {
            // Modelo de retorno.
            ModeloRetorno retorno = null;
            // BLL.
            UsuarioBLL bll = null;
            // Tentativa.
            try
            {
                // Instanciando bll.
                bll = new UsuarioBLL();
                // Ativando usuário.
                if (bll.AtivarUsuario(usuario))
                    retorno = new ModeloRetorno()
                    {
                        codigo = 0,
                        mensagem = $"Usuário '{usuario.NOMUSR}' ativado com sucesso."
                    };
                else
                    retorno = new ModeloRetorno()
                    {
                        codigo = 2,
                        mensagem = "Erro ao ativar usuário. (A tabela não foi afetada)"
                    };
            }
            catch (Exception ex)
            {
                retorno = new ModeloRetorno()
                {
                    codigo = 1,
                    mensagem = $"Erro ao ativar usuário: {ex.Message}"
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