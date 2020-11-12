using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace PrecLicenseGenerator
{
    public class PCWCryptographer
    {
        private const string initVector = "PrecisionWaveAG#";

        /// <summary>
        /// Encrypts or decrypts the specified data using Xor
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        public static void EncryptDecryptXOR(byte[] data, byte[] key)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (key == null)
                throw new ArgumentNullException("key");
            if (key.Length == 0)
                throw new ArgumentOutOfRangeException("key cannot be empty");

            // do the xor-ing
            for (int i = 0; i < data.Length; i++)
                data[i] = (byte)(data[i] ^ key[i % key.Length]);
        }


        /// <summary>
        /// Encrypts bytes with a given password.
        /// </summary>
        /// <param name="clearText">The clear text.</param>
        /// <param name="password">The password.</param>
        /// <param name="initVector" optional>Init Vector</param>
        public static byte[] EncryptByteArray(byte[] clearBytes, byte[] password, string initVector = PCWCryptographer.initVector)
        {
            // Check arguments.
            if (clearBytes == null || clearBytes.Length <= 0)
                throw new ArgumentNullException("clearBytes");
            if (password == null || password.Length <= 0)
                throw new ArgumentNullException("password");
            if (initVector == null || initVector.Length <= 0)
                throw new ArgumentNullException("initVector");

            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            var algorithm = new RijndaelManaged();
            algorithm.Mode = CipherMode.ECB;
            algorithm.BlockSize = 128;
            algorithm.KeySize = 128;
            algorithm.FeedbackSize = 8;
            algorithm.Padding = PaddingMode.None;
            var encryptor = algorithm.CreateEncryptor(password, initVectorBytes);
            using (var ms = new MemoryStream())
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                cs.Write(clearBytes, 0, clearBytes.Length);
                cs.Close();
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Decrypts a string using a given password.
        /// </summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <param name="password">The password.</param>
        /// <param name="initVector" optional>Init Vector</param>
        public static byte[] DecryptByteArray(byte[] cipherBytes, byte[] password, string initVector = PCWCryptographer.initVector)
        {
            // Check arguments.
            if (cipherBytes == null || cipherBytes.Length <= 0)
                throw new ArgumentNullException("cipherBytes");
            if (password == null || password.Length <= 0)
                throw new ArgumentNullException("password");
            if (initVector == null || initVector.Length <= 0)
                throw new ArgumentNullException("initVector");

            // do the work
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            var algorithm = new RijndaelManaged();
            algorithm.Mode = CipherMode.CFB;
            algorithm.BlockSize = 128;
            algorithm.KeySize = 128;
            algorithm.FeedbackSize = 8;
            algorithm.Padding = PaddingMode.None;
            var decryptor = algorithm.CreateDecryptor(password, initVectorBytes);
            using (var ms = new MemoryStream())
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
            {
                cs.Write(cipherBytes, 0, cipherBytes.Length);
                cs.Close();
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Encrypts a string with a given password.
        /// </summary>
        /// <param name="plainText">The clear text.</param>
        /// <param name="Key">The password.</param>
        /// <param name="IV" optional>Init Vector</param>
        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }

        /// <summary>
        /// Decrypts a string with a given password.
        /// </summary>
        /// <param name="cipherText">The clear text.</param>
        /// <param name="Key">The password.</param>
        /// <param name="IV" optional>Init Vector</param>
        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting  stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

    }
}