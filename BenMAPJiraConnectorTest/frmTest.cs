using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace BenMAPJiraConnectorTest
{
    public partial class frmTest : Form
    {
        private const string FILE_NAME = "BenMAPJiraConnector.dll";
        string path;

        public frmTest()
        {
            InitializeComponent();
        }

        private void frmTest_Load(object sender, EventArgs e)
        {
            //dll path
            path = AppDomain.CurrentDomain.BaseDirectory + @"\" + FILE_NAME;

            if (!File.Exists(path))
            {
                btnDecrypt.Enabled = false;
                return;
            }

        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            //load dll
            var connectorDLL = Assembly.LoadFile(path);
            var type = connectorDLL.GetType("BenMAPJiraConnector.Connection");
            //Now you can use dynamic to call the method.
            dynamic connection = Activator.CreateInstance(type);


            txtURL.Text = connection.GetURL();
            txtUsername.Text = connection.GetUsername();
            txtPassword.Text = connection.GetPassword();
            txtProjectKey.Text = connection.GetProjectKey();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
