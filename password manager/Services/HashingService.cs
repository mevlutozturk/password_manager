using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace password_manager.Services
{
    interface hash
    {
        string md5hash(byte[] text);
        string sha256Hash(string item);
    }
    class HashingService
    {
        public string md5hash(byte[] text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(text);
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }
        public string sha256Hash(string item)
        {
            byte[] rawData = ASCIIEncoding.ASCII.GetBytes(item);
            SHA256 sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(rawData);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
