using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrimeTeamProjectsApi.Business
{
    /// <summary>
    /// Classe de Query's.
    /// </summary>
    public class AtividadeDALSQL
    {
        /// <summary>
        /// QUERY: Obtem as atividades cadastradas no sistema.
        /// </summary>
        /// <param name="codAtv">Código da atividade.</param>
        /// <param name="nomAtv">Nome da atividade.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public string ObterAtividades(int codAtv, string nomAtv)
        {
            return $@"SELECT 
                     	CODATV,
                     	NOMATV,
                     	DESATV,
                     	TMPESTATV,
                     	DATINIATV,
                     	DATFIMATV,
                     	CONVERT(VARCHAR, DATINIATV, 103) AS DATINIATVSTR,
                     	CONVERT(VARCHAR, DATFIMATV, 103) AS DATFIMATVSTR,
                        (CASE WHEN DATFIMATV IS NULL
                            'Em execução'
                        ELSE
                            'Finalizada'
                        END) AS STATUS
                     FROM ATIVIDADE
                     WHERE 1 = 1
                     {(codAtv != -1 ? "AND CODATV = @CODATV" : string.Empty)}
                     {(nomAtv != null && nomAtv.Trim().Length > 0 ? "AND NOMATV LIKE @NOMATV" : string.Empty)}";
        }

        /// <summary>
        /// QUERY: Verifica se a atividade está relacionada a algum projeto.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public string VerificarProjetos()
        {
            return @"SELECT COUNT(*) AS EXISTE
                     	FROM ATIVIDADE ATV
                     INNER JOIN ATVUSRPRJ AUP ON AUP.CODATV = ATV.CODATV 
                     WHERE ATV.CODATV = @CODATV";
        }

        /// <summary>
        /// QUERY: Insere uma nova atividade.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public string InserirAtividade()
        {
            return @"INSERT INTO ATIVIDADE (
                     	NOMATV,
                     	DESATV,
                     	TMPESTATV,
                     	DATINIATV,
                     	DATFIMATV
                     ) VALUES (
                     	@NOMATV,
                     	@DESATV,
                     	@TMPESTATV,
                     	@DATINIATV,
                     	@DATFIMATV
                     )";
        }

        /// <summary>
        /// QUERY: Atualiza os dados da atividade.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public string AtualizarAtividade()
        {
            return @"UPDATE ATIVIDADE SET
                     	NOMATV = @NOMATV,
                     	DESATV = @DESATV,
                     	TMPESTATV = @TMPESTATV,
                     	DATINIATV = @DATINIATV,
                     	DATFIMATV = @DATFIMATV
                     WHERE CODATV = @CODATV";
        }

        /// <summary>
        /// QUERY: Atualiza as horas trabalhadas na atividade.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public string AtualizarHoras()
        {
            return @"UPDATE ATIVIDADE SET
                     	TMPESTATV = @TMPESTATV
                     WHERE CODATV = @CODATV";
        }

        /// <summary>
        /// QUERY: Deleta a atividade.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public string RemoverAtividade()
        {
            return "DELETE ATIVIDADE WHERE CODATV = @CODATV";
        }
    }
}