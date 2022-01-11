using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CryptoWPS
{
    public class ControleCriptografia
    {
        /// <summary>
        /// Critografar senha para BD
        /// </summary>
        /// <param name="passowrd">Senha do cliente</param>
        public static string CriptografaSenha(string passowrd)
        {
            HashAlgorithm sha1 = SHA1.Create();
            byte[] aaa = sha1.ComputeHash(Encoding.ASCII.GetBytes(passowrd));
            return Convert.ToBase64String(aaa);
        }
    }
}
