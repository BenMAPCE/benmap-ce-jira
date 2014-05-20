using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BenMAPJiraConnector
{
    public class Connection
    {
        //default values
        private const string ENCRYPTION_KEY = "o111111111222222222233333333334T";
        private const string ENCRYPTION_IV = "P555555555666666666677777777778u";
        private const string FILE_NAME = "BenMAPJiraConnector.txt";

        private enum LineIndex { URL, USERNAME, PASSWORD, PROJECT_KEY };

        private string GetDecrypted(LineIndex lineIndex)
        {
            string encrypted = null;
            string decrypted = null;

            encrypted = ReadLine((int)lineIndex);

            if (encrypted != null)
            {
                //get bytes
                byte[] encryptedBytes = System.Convert.FromBase64String(encrypted);
                byte[] encryptionKeyBytes = System.Text.Encoding.UTF8.GetBytes(ENCRYPTION_KEY);
                byte[] encryptionIVBytes = System.Text.Encoding.UTF8.GetBytes(ENCRYPTION_IV);

                //decrypt
                Decrypt objDecrypt = new Decrypt();
                decrypted = objDecrypt.DecryptStringFromBytes(encryptedBytes, encryptionKeyBytes, encryptionIVBytes);
            }

            return decrypted;
           
        
        }

        private string ReadLine(int index)
        {
            string line = null;

            string path = AppDomain.CurrentDomain.BaseDirectory + @"\" + FILE_NAME;

            try
            {
                if (!File.Exists(path))
                {
                    return null;
                }

                using (StreamReader sr = new StreamReader(path))
                {
                    int i = 0;
                    while (i <= index)
                    {
                        line = sr.ReadLine();
                        i++;
                    }

                    return line;
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public string GetURL()
        {
            return GetDecrypted(LineIndex.URL);
        }

        public string GetUsername()
        {
            return GetDecrypted(LineIndex.USERNAME);
        }

        public string GetPassword()
        {
            return GetDecrypted(LineIndex.PASSWORD);
        }

        public string GetProjectKey()
        {
            return GetDecrypted(LineIndex.PROJECT_KEY);
        }

        


    }
}
