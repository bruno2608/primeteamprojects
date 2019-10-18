using System;
using System.Web;
using System.Linq;
using System.Web.Http;
using System.Collections.Generic;
using PrimeTeamProjectsApi.Models;
using PrimeTeamProjectsApi.Business;
using PrimeTeamProjectsApi.Classes.Util;

namespace PrimeTeamProjectsApi.Controllers
{
    /// <summary>
    /// Controller da página de login.
    /// </summary>
    [RoutePrefix("api/Login")]
    public class LoginController : ApiController
    {
        /// <summary>
        /// Efetua o login
        /// </summary>
        /// <param name="usrId">ID do usuário.</param>
        /// <param name="usrPsw">Senha do usuário.</param>
        /// <returns>Lista de usuários.</returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        [AllowAnonymous, HttpGet, Route("Login")]
        public IHttpActionResult Login(string usrId, string usrPsw)
        {
            // Modelo de retorno.
            ModeloRetorno<Usuario> retorno = null;
            // BLL.
            UsuarioBLL bll = null;
            // Tentativa.
            try
            {
                // Instanciando bll.
                bll = new UsuarioBLL();
                // Obtendo o usuário.
                Usuario usuario = bll.ObterUsuario(-1, usrId, null, null).FirstOrDefault();
                // Verificando usuário.
                if (usuario != null)
                {
                    // String padrão de usuário pré cadastrado.
                    if (usrPsw == "ssdfd" && usuario.USRPSW == "ssdfd")
                    {
                        retorno = new ModeloRetorno<Usuario>()
                        {
                            codigo = 1,
                            mensagem = "Você já foi pré cadastrado no sistema. Favor definir sua senha.",
                            objeto = usuario
                        };
                    }
                    else
                    {
                        // Criptografando a senha e comparando.
                        if (Security.EncryptMD5(usrPsw) == usuario.USRPSW)
                        {
                            // Usuário logado com sucesso.
                            retorno = new ModeloRetorno<Usuario>()
                            {
                                codigo = 0,
                                objeto = usuario
                            };
                            // Colocando usuário na sessão.
                            HttpContext.Current.Session["USUARIO"] = usuario;
                        }
                        else
                        {
                            retorno = new ModeloRetorno<Usuario>()
                            {
                                codigo = 2,
                                mensagem = "Id ou Senha incorretos."
                            };
                        }
                    }
                }
                else
                {
                    retorno = new ModeloRetorno<Usuario>()
                    {
                        codigo = 2,
                        mensagem = "Você não está cadastrado no sistema."
                    };
                }
            }
            catch (Exception ex)
            {
                retorno = new ModeloRetorno<Usuario>()
                {
                    codigo = 3,
                    mensagem = $"Erro ao efetuar login: {ex.Message}"
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