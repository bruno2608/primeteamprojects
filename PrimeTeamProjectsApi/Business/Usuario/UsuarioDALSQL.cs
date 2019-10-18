using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrimeTeamProjectsApi.Business
{
    /// <summary>
    /// Classe de Query's.
    /// </summary>
    public class UsuarioDALSQL
    {
        /// <summary>
        /// QUERY: Obtem os usuários cadastrados.
        /// </summary>
        /// <param name="codUsr">Código do usuário.</param>
        /// <param name="usrId">ID do usuário.</param>
        /// <param name="usrPsw">Senha do usuário.</param>
        /// <param name="nomUsr">Nome do usuário.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public string ObterUsuario(int codUsr, string usrId, string usrPsw, string nomUsr)
        {
            return $@"SELECT CODUSR,
                     	   USRID,
                     	   USRPSW,
                     	   NOMUSR,
                     	   USRMAIL,
                     	   TIPUSR,	    
                     	   DATCAD, 
                     	   DATDST,
                     	   (CASE WHEN DATDST IS NULL THEN
                     			'Ativado'
                     		ELSE
                     			'Desativado'
                     	   END) AS STATUS
                     FROM USUARIO
                     	WHERE 1 = 1
                     	{(codUsr != -1 ? "AND CODUSR = @CODUSR" : string.Empty)}
                     	{(usrId != null && usrId.Trim().Length > 0 ? "AND USRID = @USRID" : string.Empty)}
                     	{(usrPsw != null && usrPsw.Trim().Length > 0 ? "AND USRPSW = @USRPSW" : string.Empty)}
                     	{(nomUsr != null && nomUsr.Trim().Length > 0 ? "AND NOMUSR = @NOMUSR" : string.Empty)}";
        }

        /// <summary>
        /// QUERY: Verifica se o usuário está relacionado com alguma atividade.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public string VerificarAtividades()
        {
            return @"SELECT COUNT(*) AS EXISTE
                     	FROM ATIVIDADE ATV
                     INNER JOIN ATVUSRPRJ AUP ON AUP.CODATV = ATV.CODATV
                     WHERE ATV.DATFIMATV IS NULL
                     	AND AUP.CODUSR = @CODUSR";
        }

        /// <summary>
        /// QUERY: Efetua a inserção de um novo usuário.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public string InserirUsuario()
        {
            return @"INSERT INTO USUARIO (
                     	USRID,
                     	USRPSW,
                        NOMUSR,
                        USRMAIL
                     ) VALUES (
                     	@USRID,
                     	@USRPSW,
                     	@NOMUSR,
                        @USRMAIL
                     )";
        }

        /// <summary>
        /// QUERY: Atualiza os dados do usuário.
        /// </summary>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public string AtualizarUsuario()
        {
            return @"UPDATE USUARIO SET
                     	USRID = @USRID
                     	,USRPSW = @USRPSW
                     	,NOMUSR = @NOMUSR
                     	,USRMAIL = @USRMAIL
                     	,TIPUSR = @TIPUSR
                     WHERE CODUSR = @CODUSR";
        }

        /// <summary>
        /// QUERY: Desativa um usuário.
        /// </summary>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public string DesativarUsuario()
        {
            return @"UPDATE USUARIO 
                     	SET DATDST = SYSDATETIME()
                     WHERE CODUSR = @CODUSR";
        }

        /// <summary>
        /// QUERY: Ativa um usuário.
        /// </summary>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public string AtivarUsuario()
        {
            return @"UPDATE USUARIO 
                     	SET DATDST = NULL
                     WHERE CODUSR = @CODUSR";
        }
    }
}