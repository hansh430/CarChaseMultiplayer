using UnityEngine;

namespace Extensions
{
    public static class SessionCodeGenerator
    {
        public static string GenerateSessionCode(int length = 4)
        {
            char[] chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789".ToCharArray();

            string str = "";
            for (int i = 0; i < length; i++)
            {
                str += chars[Random.Range(0, chars.Length)];
            }
            return str;
        }
    }
}
