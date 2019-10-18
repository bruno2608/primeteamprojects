using System;
using System.Data;
using System.Reflection;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace PrimeTeamProjectsApi.Business
{
    /// <summary>
    /// Extensões
    /// </summary>
    public static class Extensoes
    {
        /// <summary>
        /// Obtem o retorno tabela em lista.
        /// </summary>
        /// <typeparam name="T">Tipo da lista.</typeparam>
        /// <param name="sqlCommand">Comando a ser executado.</param>
        /// <returns>Lista de T(classe) selecionada.</returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public static List<T> ExecuteList<T>(this SqlCommand sqlCommand)
        {
            // Leitor.
            IDataReader reader = null;
            // Propriedades do modelo.
            PropertyInfo[] propInfos = null;
            // Lista de retorno do modelo.
            List<T> lstModelo = null;
            // Lista de colunas do reader.
            List<string> colunas = null;
            // Tentativa.
            try
            {
                // Obtendo o leitor.
                reader = sqlCommand.ExecuteReader();
                // Obtendo todas as propriedades do modelo.
                propInfos = typeof(T).GetProperties();
                // Criando a lista de retorno do modelo.
                lstModelo = new List<T>();
                // Lista de colunas do reader.
                colunas = new List<string>();
                // Obtendo as colunas do reader.
                for (int i = 0; i < reader.FieldCount; i++) colunas.Add(reader.GetName(i));
                // Obtendo os valores do reader.
                while (reader.Read())
                {
                    // Criando objeto do modelo.
                    T modelo = Activator.CreateInstance<T>();
                    // Para cada propriedade do modelo.
                    foreach (PropertyInfo prop in propInfos)
                    {
                        // Verificando quais propriedades existem no reader.
                        if (colunas.Contains(prop.Name))
                        {
                            // Obtendo o valor da coluna.
                            object valorColuna = reader[prop.Name];
                            // Verificando o valor da coluna.
                            if (valorColuna != null && valorColuna != DBNull.Value)
                            {
                                // Setando o valor da coluna do reader no modelo.
                                prop.SetValue(modelo, valorColuna);
                            }
                        }
                    }
                    // Adicionando registro na lista.
                    lstModelo.Add(modelo);
                }
                // Retornando o resultado multiplo.
                return lstModelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Limpando variáveis.
                if (reader != null)
                {
                    reader.Dispose();
                    reader = null;
                }
                if (sqlCommand != null)
                {
                    sqlCommand.Dispose();
                    sqlCommand = null;
                }
                lstModelo = null;
                propInfos = null;
                colunas = null;
                // Limpando memória.
                GC.Collect();
            }
        }

        /// <summary>
        /// Adiciona parâmetros no SqlCommand.
        /// </summary>
        /// <param name="dbCommand">Comando SQL.</param>
        /// <param name="parameterName">Nome do parâmetro.</param>
        /// <param name="parameterValue">Valor do parâmetro.</param>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public static void AddParameter(this SqlCommand dbCommand, string parameterName, object parameterValue)
        {
            // Parâmetro.
            SqlParameter parametro = null;
            // Tentativa.
            try
            {
                // Criando o parâmetro.
                parametro = dbCommand.CreateParameter();
                // Nome do parâmetro.
                parametro.ParameterName = parameterName;
                // Valor do parâmetro.
                parametro.Value = parameterValue;
                // Adicionando o novo parâmetro no DbCommand.
                dbCommand.Parameters.Add(parametro);
                // Limpando variável
                parametro = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Limpando variável.
                parametro = null;
            }
        }
    }
}