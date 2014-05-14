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
        private const string ENCRYPTION_KEY = "11111111112222222222333333333344";
        private const string ENCRYPTION_IV = "55555555556666666666777777777788";
        private const string FILE_NAME = "BenMAPJiraConnector.txt";

        public string GetURL()
        {
            string encrypted = "";
            string decrypted = "";

            return decrypted;        
        }

        public string GetUsername()
        {
            string encrypted = "";
            string decrypted = "";

            return decrypted; 
        }

        public string GetPassword()
        {
            string encrypted = "";
            string decrypted = "";

            return decrypted; 
        }

        public string GetProjectKey()
        {
            string encrypted = "";
            string decrypted = "";

            return decrypted; 
        }

        private string ReadLine(int index)
        {
            string line = "";

            string path = FILE_NAME;

            try
            {
                if (!File.Exists(path))
                {
                    return line;
                }

                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        Console.WriteLine(sr.ReadLine());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }



            return line;


            
        }


    }
}
