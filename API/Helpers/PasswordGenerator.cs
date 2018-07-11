using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace API.Helpers
{
    public class PasswordGenerator
    {
        public static string GenerateRandomAlphanumericString(int length)
        {
            const string alphanumericCharacters =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                "abcdefghijklmnopqrstuvwxyz" +
                "0123456789" +
                "!#$%&/()=?";
            return GenerateRandomString(length, alphanumericCharacters);
        }

        public static string GenerateRandomAlphanumericStringWithoutSpecialChar(int length)
        {
            const string alphanumericCharacters =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                "abcdefghijklmnopqrstuvwxyz" +
                "0123456789";
            return GenerateRandomString(length, alphanumericCharacters);
        }

        public static string GenerateRandomString(int length, IEnumerable<char> characterSet)
        {
            var characterArray = characterSet.Distinct().ToArray();
            var bytes = new byte[length * 8];
            new RNGCryptoServiceProvider().GetBytes(bytes);
            var result = new char[length];
            Random rnd = new Random();
            int randomNumber = rnd.Next(length);
            int randomNumber2 = rnd.Next(52, 61);
            for (int i = 0; i < length; i++)
            {
                if (i == randomNumber)
                {
                    ulong value = (ulong)randomNumber2;
                    result[i] = characterArray[value % (uint)characterArray.Length];
                }
                else
                {
                    ulong value = BitConverter.ToUInt64(bytes, i * 8);
                    result[i] = characterArray[value % (uint)characterArray.Length];
                }
            }
            return new string(result);
        }

        public static string GenerateSalt()
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[16];
            provider.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        public static string GenerateHash(string password, string salt)
        {
            byte[] _password = Encoding.Unicode.GetBytes(password);
            byte[] _salt = Convert.FromBase64String(salt);
            byte[] hashedString = new byte[_password.Length + _salt.Length];
            Buffer.BlockCopy(_password, 0, hashedString, 0, _password.Length);
            Buffer.BlockCopy(_salt, 0, hashedString, _password.Length, _salt.Length);
            return Convert.ToBase64String(HashAlgorithm.Create("SHA1").ComputeHash(hashedString));
        }
    }
}