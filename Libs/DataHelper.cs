using System.Globalization;

namespace Server.Tools
{

    public class DataHelper
    {

        public static string Bytes2HexString(byte[] b)
        {
            string ret = "";
            for (int i = 0; i < b.Length; i++)
            {
                ret += ((int)(b[i] & byte.MaxValue)).ToString("X2").ToUpper();
            }
            return ret;
        }


        public static byte[]? HexString2Bytes(string s)
        {
            byte[] result;
            if (s.Length % 2 != 0) return null;

            byte[] bytesData = new byte[s.Length / 2];
            for (int i = 0; i < s.Length / 2; i++)
            {
                string hexstr = s.Substring(i * 2, 2);
                int b = int.Parse(hexstr, NumberStyles.HexNumber) & 255;
                bytesData[i] = (byte)b;
            }
            result = bytesData;
            return result;
        }

    }
}
