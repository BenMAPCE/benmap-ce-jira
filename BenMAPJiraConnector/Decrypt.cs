using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;


namespace BenMAPJiraConnector
{
    class Decrypt
    {

        private const int BLOCK_SIZE = 256;
        private const int KEY_SIZE = 256;
           
        public Decrypt()
        {      
        
        }

        private bool IsValid(byte[] plainText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (plainText == null || plainText.Length <= 0)
            {
                return false;
            }
            if (Key == null || Key.Length <= 0)
            {
                return false;
            }
            if (IV == null || IV.Length <= 0)
            {
                return false;
            }

            //check key, iv lengths
            if (Key.Length != (KEY_SIZE / 8))
            {
                return false;
            }

            if (IV.Length != (BLOCK_SIZE / 8))
            {
                return false;
            }


            return true;
        }

        private bool IsValid(string plainText, byte[] Key, byte[] IV) 
        {
            // Check arguments. 
            if (plainText == null || plainText.Length <= 0)
            {
                return false;
            }
            if (Key == null || Key.Length <= 0)
            {
                return false;
            }
            if (IV == null || IV.Length <= 0)
            {
                return false;
            }

            //check key, iv lengths
            if (Key.Length != (KEY_SIZE / 8))
            {
                return false;
            }

            if (IV.Length != (BLOCK_SIZE / 8))
            {
                return false;
            }


            return true;
        }

        public byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {

            if (!IsValid(plainText, Key, IV))
            {
                return null;
            }

            byte[] encrypted;
            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.BlockSize = BLOCK_SIZE;
                rijAlg.KeySize = KEY_SIZE;

                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

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

        public string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {

            if (!IsValid(cipherText, Key, IV))
            {
                return null;
            }

            // Declare the string used to hold 
            // the decrypted text. 
            string plaintext = null;

            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {

                rijAlg.BlockSize = BLOCK_SIZE;
                rijAlg.KeySize = KEY_SIZE;

                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption. 
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream 
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
