using System;
using System.Text;
using System.Security.Cryptography;

namespace PrimeTeamProjectsApi.Classes.Util
{
    /// <summary>
    /// Classe de criptografia de senha.
    /// </summary>
    public static class Security
    {
        /// <summary>
        /// Criptografa o texto em MD5.
        /// </summary>
        /// <param name="source">Texto a ser criptografado.</param>
        /// <returns>Texto criptografado em MD5.</returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public static string EncryptMD5(string source)
        {
            // Provedor MD5.
            MD5 md5 = null;
            // Resultado.
            byte[] hash = null;
            // Retorno.
            string md5String = null;
            // Tentativa.
            try
            {
                // Criando MD5.
                md5 = new MD5CryptoServiceProvider();
                // Computando bytes da string.
                hash = md5.ComputeHash(Encoding.ASCII.GetBytes(source));
                // Obtendo string.
                for (int i = 0; i < hash.Length; i++)
                    md5String = $"{md5String}{hash[i].ToString("x2")}";
                // Retornando string montada.
                return md5String;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}