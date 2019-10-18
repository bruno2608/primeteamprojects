using System;
using System.Linq;
using System.Data;
using System.Reflection;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace PrimeTeamProjectsApi.Business
{
    /// <summary>
    /// Classe de conexão abstrata.
    /// </summary>
    public abstract class DAL
    {

        #region Propriedades
        /// <summary>
        /// Conexão auxíliar.
        /// </summary>
        private static SqlConnection _connection = null;
        /// <summary>
        /// Conexão padrão com o banco de dados.
        /// </summary>
        protected SqlConnection connection
        {
            get
            {
                // Verificando conexão.
                if (_connection == null || _connection.State == ConnectionState.Closed)
                {
                    // Criando nova conexão.
                    _connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
                    // Abrindo a conexão.
                    _connection.Open();
                }
                // Retornando conexão.
                return _connection;
            }
        }
        #endregion

        #region Funções
        /// <summary>
        /// Cria o comando sql.
        /// </summary>
        /// <param name="sqlCommand">Query a ser executada</param>
        /// <returns></returns>
        protected SqlCommand GetCommand(string sqlCommand)
        {
            // Commando.
            SqlCommand command = null;
            // Tentativa.
            try
            {
                // Instanciando commando.
                command = new SqlCommand(sqlCommand, this.connection);
                // Definindo tipo do comando.
                command.CommandType = CommandType.Text;
                // Retornando comando.
                return command;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Limpando variável.
                command = null;
                // Limpando memória.
                GC.Collect();
            }
        }
        #endregion

    }
}