using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrimeTeamProjectsApi.Business
{
    /// <summary>
    /// Classe de Query's.
    /// </summary>
    public class ProjetoDALSQL
    {
        #region Projeto
        /// <summary>
        /// QUERY: Obtem os projetos cadastrados no sistema.
        /// </summary>
        /// <param name="codPrj">Código do projeto.</param>
        /// <param name="nomPrj">Nome do projeto.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public string ObterProjetos(int codPrj, string nomPrj)
        {
            return $@"SELECT 
                     	CODPRJ,
                     	NOMPRJ,
                     	DESPRJ,
                     	DATINIPRJ,
                        DATFIMPRJ,
                        CONVERT(VARCHAR, DATINIPRJ, 103) AS DATINISTR,
                        CONVERT(VARCHAR, DATFIMPRJ, 103) AS DATFIMSTR,
                     		(CASE WHEN DATINIPRJ > SYSDATETIME() THEN
                                'Aguardando'
                            WHEN DATFIMPRJ IS NULL THEN
                     			'Em execução' 
                     		ELSE 
                     			'Finalizado' 
                     		END) AS STATUS
                         FROM PROJETO
                     WHERE 1 = 1
                     {(codPrj != -1 ? "AND CODPRJ = @CODPRJ" : string.Empty)}
                     {(nomPrj != null && nomPrj.Trim().Length > 0 ? "AND NOMPRJ LIKE @NOMPRJ" : string.Empty)}";
        }

        /// <summary>
        /// QUERY: Obtem o próximo código da tabela de projeto.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public string ObterProximoProjeto()
        {
            return "SELECT IDENT_CURRENT('PROJETO') + 1 AS PRXCODPRJ";
        }

        /// <summary>
        /// QUERY: Efetua a inclusão de um novo projeto.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public string InserirProjeto()
        {
            return @"INSERT INTO PROJETO (
                     	NOMPRJ,
                     	DESPRJ,
                     	DATINIPRJ,
                     	DATFIMPRJ
                     ) VALUES (
                     	@NOMPRJ,
                     	@DESPRJ,
                     	@DATINIPRJ,
                     	@DATFIMPRJ
                     )";
        }

        /// <summary>
        /// QUERY: Atualiza os dados do projeto.
        /// </summary>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public string AtualizarProjeto()
        {
            return @"UPDATE PROJETO SET
                     	NOMPRJ = @NOMPRJ
                     	,DESPRJ = @DESPRJ
                     	,DATINIPRJ = @DATINIPRJ
                     	,DATFIMPRJ = @DATFIMPRJ
                     WHERE CODPRJ = @CODPRJ";
        }
        #endregion

        #region TipoProjeto

        /// <summary>
        /// QUERY: Obtem os tipos de projeto.
        /// </summary>
        /// <param name="codTip">Código do tipo de projeto.</param>
        /// <param name="nomTip">Nome do tipo de projeto.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public string ObterTiposProjeto(int codTip, string nomTip)
        {
            return $@"SELECT 
                     	CODTIP,
                     	NOMTIP,
                     	DESTIP
                     FROM TIPOPROJETO
                     WHERE 1 = 1
                     {(codTip != -1 ? "AND CODTIP = @CODTIP" : string.Empty)}
                     {(nomTip != null && nomTip.Trim().Length > 0 ? "AND NOMTIP LIKE @NOMTIP" : string.Empty)}";
        }

        /// <summary>
        /// QUERY: Verifica se o tipo de projeto já está sendo utilizado.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public string VerificarProjetos()
        {
            return @"SELECT COUNT(*) EXISTE
                     	FROM TIPOPROJETO TIP
                     INNER JOIN PROJETOXTIPO PXT ON PXT.CODTIP = TIP.CODTIP
                     WHERE TIP.CODTIP = @CODTIP";
        }

        /// <summary>
        /// QUERY: Insere um novo tipo de projeto.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public string InserirTipoProjeto()
        {
            return @"INSERT INTO TIPOPROJETO (
                     	NOMTIP,
                     	DESTIP
                     ) VALUES (
                     	@NOMTIP,
                     	@DESTIP
                     )";
        }

        /// <summary>
        /// QUERY: Atualiza os dados do tipo de projeto.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public string AtualizarTipoProjeto()
        {
            return @"UPDATE TIPOPROJETO SET
                     	NOMTIP = @NOMTIP,
                     	DESTIP = @DESTIP
                     WHERE CODTIP = @CODTIP";
        }

        /// <summary>
        /// QUERY: Deleta o tipo do projeto.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public string RemoverTipoProjeto()
        {
            return "DELETE TIPOPROJETO WHERE CODTIP = @CODTIP";
        }
        #endregion

        #region Projeto x Tipo
        /// <summary>
        /// QUERY: Obtem relação entre um projeto e um tipo.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public string ObterRelacaoPrjTip()
        {
            return @"SELECT TIP.CODTIP,
                     	   TIP.NOMTIP,
                     	   TIP.DESTIP
                     	FROM TIPOPROJETO TIP
                     INNER JOIN PROJETOXTIPO PXT ON PXT.CODTIP = TIP.CODTIP
                     WHERE PXT.CODPRJ = @CODPRJ";
        }

        /// <summary>
        /// QUERY: Insere uma relação entre um projeto e um tipo.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public string InserirRelacaoPrjTip()
        {
            return @"INSERT INTO PROJETOXTIPO (
                     	CODPRJ,
                     	CODTIP
                     ) VALUES (
                     	@CODPRJ,
                     	@CODTIP
                     )";
        }

        /// <summary>
        /// QUERY: Delete a relação entre um projeto e um tipo.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public string RemoverRelacaoPrjTip()
        {
            return @"DELETE FROM PROJETOXTIPO
                     WHERE CODPRJ = @CODPRJ
                       AND CODTIP = @CODTIP";
        }
        #endregion

        #region Atividade x Usuario x Projeto

        /// <summary>
        /// QUERY: Obtem relação entre atividade x usuário x projeto.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public string ObterRelacaoAUP()
        {
            return @"SELECT 
                     	AUP.CODATVUSRPRJ,
                     	PRJ.CODPRJ,
                     	USR.CODUSR,
                     	USR.NOMUSR,
                     	ATV.CODATV,
                     	ATV.NOMATV,
                     	ATV.DESATV,
                        CONVERT(VARCHAR, ATV.DATINIATV, 103) AS DATINIATVSTR,
                     	CONVERT(VARCHAR, ATV.DATFIMATV, 103) AS DATFIMATVSTR,
                        ATV.TMPESTATV
                     FROM ATVUSRPRJ AUP
                     	INNER JOIN PROJETO PRJ ON PRJ.CODPRJ = AUP.CODPRJ
                     	INNER JOIN USUARIO USR ON USR.CODUSR = AUP.CODUSR
                     	INNER JOIN ATIVIDADE ATV ON ATV.CODATV = AUP.CODATV
                     WHERE PRJ.CODPRJ = @CODPRJ
                     	ORDER BY USR.CODUSR, ATV.CODATV";
        }

        /// <summary>
        /// QUERY: Insere uma nova relação entre atividade x usuário x projeto.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public string InserirRelacaoAUP()
        {
            return @"INSERT INTO ATVUSRPRJ (
                     	CODATV,
                     	CODPRJ,
                     	CODUSR
                     ) VALUES (
                     	@CODATV,
                     	@CODPRJ,
                     	@CODUSR
                     )";
        }

        /// <summary>
        /// QUERY: Remove a relação entre atividade x usuário x projeto.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public string RemoverRelacaoAUP()
        {
            return @"DELETE ATVUSRPRJ
                     WHERE CODATV = @CODATV
                       AND CODPRJ = @CODPRJ
                       AND CODUSR = @CODUSR";
        }
        #endregion
    }
}