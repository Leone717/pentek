using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace MenuEsSql
{
    class titkositas
    {
        //SHA256 titkosítási metódusa
        public static string SHA2Hash(string inputString)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        //SHA512 titkosítási metódusa
        public static string SHA5Hash(string inputString)
        {
            SHA512 sha512 = SHA512Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i<hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));//nagy X2 művelet!
            }
            return result.ToString();
        }

        //MD5 titkosítási metódusa

        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            //get hash result after compute it
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i< result.Length; i++)
            {
                //minden byteot átalakít egy hexadecimális számmá(00 - FF)
                strBuilder.Append(result[i].ToString("x2"));//kicsi x2, bit szintű művelet!
            }
            return strBuilder.ToString();
        }
    }
}
