using System;
using System.Text;

namespace Server.Tools
{

    public class StringEncrypt
    {

        public static string? Encrypt(string plainText, string passwd, string saltValue)
        {
            string? result;
            if (string.IsNullOrEmpty(plainText)) return null;

            byte[]? bytesData = null;
            try
            {
                bytesData = new UTF8Encoding().GetBytes(plainText);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Encrypt - GetBytes failed: {0}", ex.Message);
                throw;
            }
            byte[]? bytesResult = null;
            try
            {
                bytesResult = AesHelper.AesEncryptBytes(bytesData, passwd, saltValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Encrypt - AesEncryptBytes failed: {0}", ex.Message);
                throw;
            }
            result = DataHelper.Bytes2HexString(bytesResult);
            return result;
        }


        public static string? Decrypt(string encryptText, string passwd, string saltValue)
        {
            string? result;
            if (string.IsNullOrEmpty(encryptText)) return null;

            byte[]? bytesData = DataHelper.HexString2Bytes(encryptText);
            if (null == bytesData) return null;

            byte[]? bytesResult = null;
            try
            {
                bytesResult = AesHelper.AesDecryptBytes(bytesData, passwd, saltValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Decrypt - AesDecryptBytes failed: {0}", ex.Message);
                throw;
            }
            if (bytesResult == null) return null;

            string? strResult = null;
            try
            {
                strResult = new UTF8Encoding().GetString(bytesResult, 0, bytesResult.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Decrypt - GetString failed: {0}", ex.Message);
                throw;
            }
            result = strResult;
            return result;
        }
    }
}
