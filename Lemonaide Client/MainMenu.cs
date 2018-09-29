using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Lemonaide_Client
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            if (!File.Exists("servers.txt"))
            {
                File.Create("servers.txt").Dispose();
                MessageBox.Show("No servers.txt detected. The Launcher will now create one.", "Lemonaide Launcher - Error while loading Main Menu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (!File.Exists("ports.txt"))
            {
                File.Create("ports.txt").Dispose();
                MessageBox.Show("No ports.txt detected. The Launcher will now create one.", "Lemonaide Launcher - Error while loading Main Menu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            InitializeComponent();
        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
