using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

#pragma warning disable SYSLIB0021
#pragma warning disable SYSLIB0041

namespace Server.Tools
{

    internal class AesHelper
    {

        public static byte[] AesEncryptBytes(byte[] data, string passwd, string saltValue)
        {
            byte[] saltBytes = Encoding.UTF8.GetBytes(saltValue);
            var aes = new AesManaged();
            aes.Mode = CipherMode.CBC;
            var rfc = new Rfc2898DeriveBytes(passwd, saltBytes);
            aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
            aes.KeySize = aes.LegalKeySizes[0].MaxSize;
            aes.Key = rfc.GetBytes(aes.KeySize / 8);
            aes.IV = rfc.GetBytes(aes.BlockSize / 8);
            var encryptTransform = aes.CreateEncryptor();
            MemoryStream encryptStream = new MemoryStream();
            CryptoStream encryptor = new CryptoStream(encryptStream, encryptTransform, CryptoStreamMode.Write);
            encryptor.Write(data, 0, data.Length);
            encryptor.Close();
            return encryptStream.ToArray();
        }


        public static byte[]? AesDecryptBytes(byte[] encryptData, string passwd, string saltValue)
        {
            byte[] saltBytes = Encoding.UTF8.GetBytes(saltValue);
            var aes = new AesManaged();
            var rfc = new Rfc2898DeriveBytes(passwd, saltBytes);
            aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
            aes.KeySize = aes.LegalKeySizes[0].MaxSize;
            aes.Key = rfc.GetBytes(aes.KeySize / 8);
            aes.IV = rfc.GetBytes(aes.BlockSize / 8);
            ICryptoTransform decryptTransform = aes.CreateDecryptor();
            MemoryStream decryptStream = new MemoryStream();
            CryptoStream decryptor = new CryptoStream(decryptStream, decryptTransform, CryptoStreamMode.Write);
            try
            {
                decryptor.Write(encryptData, 0, encryptData.Length);
                decryptor.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("AesDecryptBytes failed: {0}", ex.Message);
                throw;
            }
            return decryptStream.ToArray();
        }
    }
}
