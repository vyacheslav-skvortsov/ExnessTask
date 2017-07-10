using System;
using System.Security.Cryptography;

namespace ExnessTask.Helpers
{
    public class RandomHelper
    {
        public static string GetRandomString(int size)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var bytes = new byte[size];
            var provider = new RNGCryptoServiceProvider();
            provider.GetBytes(bytes);

            var characters = new char[size];

            for (int i = 0; i < size; i++)
            {
                characters[i] = chars[bytes[i] % chars.Length];
            }

            return new string(characters);
        }

        public static string GetRandomString(int size, string prefix = null, string postfix = null)
        {
            return String.Format("{0}{1}{2}", prefix, GetRandomString(size), postfix);
        }

        public static int GetRandomInt()
        {
            return new Random().Next(0, Int32.MaxValue);
        }

        public static string GetGuid()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
