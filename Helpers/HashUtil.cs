using System.Security.Cryptography;
using System.Text;

namespace StudentManagement.Helpers
{
    public static class HashUtil
    {
        public static string Hash(string input)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
            return Convert.ToHexString(bytes).ToLower();
        }

        public static bool Verify(string input, string hash)
        {
            return Hash(input) == hash;
        }
    }
}
