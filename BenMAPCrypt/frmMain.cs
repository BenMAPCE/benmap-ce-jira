using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace BenMAPCrypt
{
    public partial class frmMain : Form
    {

        public frmMain()
        {
            InitializeComponent();

            //default values
            txtEncryptionKey.Text = "o111111111222222222233333333334T";
            txtEncryptionIV.Text = "P555555555666666666677777777778u";

            txtURL.Text =  "https://f8nnm8p.atlassian.net/";
            txtUsername.Text = "BenMAP-CE";
            txtPassword.Text = "BenMAPOpenSource14";
            txtProjectKey.Text ="USERBUGS";

        }

        private bool IsValid()
        {

            //check for nulls, empties
            if (String.IsNullOrEmpty(txtEncryptionKey.Text.Trim()))
            {
                MessageBox.Show("Encryption Key is required.");
                return false;
            }
            else {

                if (txtEncryptionKey.Text.Trim().Length != 32)
                {
                    MessageBox.Show("Encryption Key must be 32 characters in length.");
                    return false;
                }
            
            }

            if (String.IsNullOrEmpty(txtEncryptionIV.Text.Trim()))
            {
                MessageBox.Show("Encryption IV is required.");
                return false;
            }
            else
            {

                if (txtEncryptionIV.Text.Trim().Length != 32)
                {
                    MessageBox.Show("Encryption IV must be 32 characters in length.");
                    return false;
                }

            }

            if (String.IsNullOrEmpty(txtURL.Text.Trim()))
            {
                MessageBox.Show("URL is required.");
                return false;
            }

            if (String.IsNullOrEmpty(txtUsername.Text.Trim()))
            {
                MessageBox.Show("Username is required.");
                return false;
            }

            if (String.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                MessageBox.Show("Password is required.");
                return false;
            }

            if (String.IsNullOrEmpty(txtProjectKey.Text.Trim()))
            {
                MessageBox.Show("Project Key is required.");
                return false;
            }

            if (String.IsNullOrEmpty(txtFile.Text.Trim()))
            {
                MessageBox.Show("File is required.");
                return false;
            }

            return true;
    
        
        }


        private void btnGenerateFile_Click(object sender, EventArgs e)
        {

            try
            {

                if (!IsValid())
                {
                    return;
                }

                string strEncryptionKey = txtEncryptionKey.Text.Trim();
                string strEncryptionIV = txtEncryptionIV.Text.Trim();

                //get bytes
                byte[] encryptionKey = System.Text.Encoding.UTF8.GetBytes(strEncryptionKey);
                byte[] encryptionIV = System.Text.Encoding.UTF8.GetBytes(strEncryptionIV);               

                // create crypto class
                Crypt crypto = new Crypt();               

                //open file for writing
                FileStream fs = new FileStream(txtFile.Text.Trim(), FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);               

                // Encrypt the strings to an array of bytes.
                // and write to file

                //URL
                byte[] encrypted = crypto.EncryptStringToBytes(txtURL.Text.Trim(), encryptionKey, encryptionIV);
                string strEncrypted = System.Convert.ToBase64String(encrypted);
                sw.WriteLine(strEncrypted);

                //Username
                encrypted = crypto.EncryptStringToBytes(txtUsername.Text.Trim(), encryptionKey, encryptionIV);
                strEncrypted = System.Convert.ToBase64String(encrypted);
                sw.WriteLine(strEncrypted);

                //Password
                encrypted = crypto.EncryptStringToBytes(txtPassword.Text.Trim(), encryptionKey, encryptionIV);
                strEncrypted = System.Convert.ToBase64String(encrypted);
                sw.WriteLine(strEncrypted);

                //Project Key
                encrypted = crypto.EncryptStringToBytes(txtProjectKey.Text.Trim(), encryptionKey, encryptionIV);
                strEncrypted = System.Convert.ToBase64String(encrypted);
                sw.WriteLine(strEncrypted);

                sw.Close();
                fs.Close();

                MessageBox.Show("File generated successfully!");

                // Decrypt the bytes to a string. 
                //string roundtrip = crypto.DecryptStringFromBytes(System.Convert.FromBase64String(strEncrypted), encryptionKey, encryptionIV);
                //string roundtrip = crypto.DecryptStringFromBytes(encrypted, encryptionKey, encryptionIV);
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("File generation failure!");
            }



        }


        

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            SaveFileDialog fd  = new SaveFileDialog();
            string strFileName = ""; 
            fd.Title = "Open File Dialog";
            fd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            fd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            fd.FilterIndex = 1;
            fd.RestoreDirectory = true;

            if (fd.ShowDialog() == DialogResult.OK)
            {
               strFileName = fd.FileName;
            }

            txtFile.Text = strFileName;

        }


    }
}
