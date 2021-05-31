using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace password_manager.Services
{
    class CryptoService : HashingService
    {
        private AesCryptoServiceProvider acsp = new AesCryptoServiceProvider();
        public string textEncrytion(string clearText, string key, string iv)
        {
            byte[] encryptedTextByte = encrypt(Encoding.UTF8.GetBytes(clearText), key, iv);
            return Convert.ToBase64String(encryptedTextByte);
        }
        public string textDecrytion(string encryptedText, string key, string iv)
        {
            byte[] clearTextByte = decrypt(Convert.FromBase64String(encryptedText), key, iv);
            return Encoding.UTF8.GetString(clearTextByte);
        }

        private byte[] encrypt(byte[] clearText, string key, string iv)
        {
            acsp.Key = Encoding.ASCII.GetBytes(key);
            acsp.IV = Encoding.ASCII.GetBytes(iv);
            acsp.Mode = CipherMode.CBC;
            ICryptoTransform cryptoTransform = acsp.CreateEncryptor();
            byte[] cipherTextBytes = cryptoTransform.TransformFinalBlock((clearText), 0, clearText.Length);
            return cipherTextBytes;
        }

        private byte[] decrypt(byte[] cipherText, string key, string iv)
        {
            acsp.Key = Encoding.ASCII.GetBytes(key);
            acsp.IV = Encoding.ASCII.GetBytes(iv);
            acsp.Mode = CipherMode.CBC;
            ICryptoTransform cryptoTransform = acsp.CreateDecryptor();
            byte[] clearTextBytes = cryptoTransform.TransformFinalBlock(cipherText, 0, cipherText.Length);
            return clearTextBytes;
        }
    }
}
