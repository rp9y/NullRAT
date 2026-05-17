using NullRAT.Config;
using System;
using System.Security.Cryptography;
using System.Text;

namespace NullRAT.Utils
{
    public static class Crypto
    {
        public static byte[] Encrypt(byte[] plainData)
        {
            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(Config.AESKey.PadRight(32).Substring(0, 32));
            aes.IV = Encoding.UTF8.GetBytes(Config.AESIV.PadRight(16).Substring(0, 16));
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var encryptor = aes.CreateEncryptor();
            return encryptor.TransformFinalBlock(plainData, 0, plainData.Length);
        }

        public static byte[] Decrypt(byte[] encryptedData, int length)
        {
            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(Config.AESKey.PadRight(32).Substring(0, 32));
            aes.IV = Encoding.UTF8.GetBytes(Config.AESIV.PadRight(16).Substring(0, 16));
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var decryptor = aes.CreateDecryptor();
            return decryptor.TransformFinalBlock(encryptedData, 0, length);
        }
    }
}