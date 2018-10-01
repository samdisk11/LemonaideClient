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
        private DiscordRpc.EventHandlers handlers;

        void StartDiscordMainMenu()
        {
            handlers = new DiscordRpc.EventHandlers();
            DiscordRpc.Initialize(GlobalVars.appid, ref handlers, true, "");

            GlobalVars.presence.largeImageKey = GlobalVars.imagekey_large;
            GlobalVars.presence.state = "In Launcher";
            GlobalVars.presence.details = "Main Menu";
            GlobalVars.presence.largeImageText = "Lemonaide Client Beta";
            DiscordRpc.UpdatePresence(ref GlobalVars.presence);
        }



        //stuff for the tabs, used to tell you what maps are there and your saved ports and IP's
        //very handy to have so you dont need notepad or sticky notes
        void tabControl1SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])
            {
                string clientdir = GlobalVars.ClientDir;
                DirectoryInfo dinfo = new DirectoryInfo(clientdir);
                DirectoryInfo[] Dirs = dinfo.GetDirectories();
                foreach (DirectoryInfo dir in Dirs)
                {
                    listBox2.Items.Add(dir.Name);
                }
                listBox2.SelectedItem = GlobalVars.SelectedClient;
                listBox1.Items.Clear();
                listBox3.Items.Clear();
                listBox4.Items.Clear();
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage3"])
            {
                string mapdir = GlobalVars.MapsDir;
                DirectoryInfo dinfo = new DirectoryInfo(mapdir);
                FileInfo[] Files = dinfo.GetFiles("*.rbxl");
                foreach (FileInfo file in Files)
                {
                    listBox1.Items.Add(file.Name);
                }
                listBox1.SelectedItem = GlobalVars.Map;
                listBox2.Items.Clear();
                listBox3.Items.Clear();
                listBox4.Items.Clear();
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage5"])
            {
                string[] lines_server = File.ReadAllLines("servers.txt");
                listBox3.Items.AddRange(lines_server);
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                listBox4.Items.Clear();
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage6"])
            {
                string[] lines_ports = File.ReadAllLines("ports.txt");
                listBox4.Items.AddRange(lines_ports);
                listBox1.Items.Clear();
                listBox2.Items.Clear();
            }
            else
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                listBox3.Items.Clear();
                listBox4.Items.Clear();
            }
        }

        public MainMenu()
        {
            GlobalVars.ClientDir = Path.Combine(Environment.CurrentDirectory, @"clients");
            GlobalVars.ClientDir = GlobalVars.ClientDir.Replace(@"\", @"\\");
            GlobalVars.ScriptsDir = Path.Combine(Environment.CurrentDirectory, @"scripts");
            GlobalVars.ScriptsDir = GlobalVars.ScriptsDir.Replace(@"\", @"\\");
            GlobalVars.MapsDir = Path.Combine(Environment.CurrentDirectory, @"maps");
            GlobalVars.MapsDir = GlobalVars.MapsDir.Replace(@"\", @"\\");

            StartDiscordMainMenu();
            MessageBox.Show("UNSTABLE SOFTWARE! WE ARE A WORK IN PROGRESS.", "Lemonaide Launcher - Important Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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
            int parsedValue;
            if (int.TryParse(numericUpDown1.Text, out parsedValue))
            {
                if (numericUpDown1.Text.Equals(""))
                {
                    //set it to the normal port, 53640. it wouldn't make any sense if we set it to 0.
                    GlobalVars.RobloxPort = GlobalVars.DefaultRobloxPort;
                }
                else
                {
                    GlobalVars.RobloxPort = Convert.ToInt32(numericUpDown1.Text);
                }
            }
            else
            {
                GlobalVars.RobloxPort = GlobalVars.DefaultRobloxPort;
            }
            label38.Text = GlobalVars.RobloxPort.ToString();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // lmao u a hacker
            MessageBox.Show("You found a easter egg! well done..", "Lemonaide Launcher - Easter Egg", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            GlobalVars.IP = textBox1.Text;
            label37.Text = GlobalVars.IP;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            File.AppendAllText("servers.txt", GlobalVars.IP + Environment.NewLine);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            File.AppendAllText("ports.txt", GlobalVars.RobloxPort + Environment.NewLine);
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GlobalVars.IP = listBox3.SelectedItem.ToString();
                textBox1.Text = GlobalVars.IP;
                label37.Text = GlobalVars.IP;
            }
            catch (Exception)
            {
            }
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GlobalVars.RobloxPort = Convert.ToInt32(listBox4.SelectedItem.ToString());
                numericUpDown1.Text = GlobalVars.RobloxPort.ToString();
                label38.Text = GlobalVars.RobloxPort.ToString();
            }
            catch (Exception)
            {
            }
        }
    }
}
