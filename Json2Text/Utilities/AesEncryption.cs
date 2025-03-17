using System.Security.Cryptography;

namespace Json2Text.Utilities
{
    public class AesEncryption
    {
        private const int _keySize = 256;
        private const int _saltSize = 16;
        private const int _iterations = 100000;

        public static string EncryptString(string plainText, string password)
        {
            byte[] salt = GenerateRandomBytes(_saltSize);
            using (var keyDerivationFunction = new Rfc2898DeriveBytes(password, salt, _iterations, HashAlgorithmName.SHA256))
            {
                byte[] key = keyDerivationFunction.GetBytes(_keySize / 8);
                byte[] iv = keyDerivationFunction.GetBytes(16);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        ms.Write(salt, 0, salt.Length);

                        using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        using (var sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }

                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        public static string DecryptString(string cipherText, string password)
        {
            byte[] fullCipher = Convert.FromBase64String(cipherText);
            byte[] salt = new byte[_saltSize];
            Array.Copy(fullCipher, 0, salt, 0, salt.Length);

            using (var keyDerivationFunction = new Rfc2898DeriveBytes(password, salt, _iterations, HashAlgorithmName.SHA256))
            {
                byte[] key = keyDerivationFunction.GetBytes(_keySize / 8);
                byte[] iv = keyDerivationFunction.GetBytes(16);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;

                    using (var ms = new MemoryStream(fullCipher, salt.Length, fullCipher.Length - salt.Length))
                    using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (var sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }

        private static byte[] GenerateRandomBytes(int size)
        {
            byte[] data = new byte[size];
            RandomNumberGenerator.Fill(data);
            return data;
        }
    }
}
