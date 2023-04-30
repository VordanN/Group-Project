using System.Security.Cryptography;

namespace MoodleProject.Utils
{
    public static class Utils
    {
        public static string ConvertStringtoMD5(string strword)
        {
            byte[] byteArr;

            byteArr = System.Text.Encoding.UTF8.GetBytes(strword);
            byteArr = MD5.Create().ComputeHash(byteArr);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (byte b in byteArr)
            {
                sb.Append(b.ToString("x2").ToLower());
            }
            return sb.ToString();
        }

    }
}
