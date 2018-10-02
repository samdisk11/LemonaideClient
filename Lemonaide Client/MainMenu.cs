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




        public MainMenu()
        {
          
            GlobalVars.ClientDir = Path.Combine(Environment.CurrentDirectory, @"clients");
            GlobalVars.ClientDir = GlobalVars.ClientDir.Replace(@"\\", @"\");
            GlobalVars.ScriptsDir = Path.Combine(Environment.CurrentDirectory, @"scripts");
            GlobalVars.ScriptsDir = GlobalVars.ScriptsDir.Replace(@"\\", @"\");
            GlobalVars.MapsDir = Path.Combine(Environment.CurrentDirectory, @"maps");
            GlobalVars.MapsDir = GlobalVars.MapsDir.Replace(@"\\", @"\");

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
        void StartStudio()
        {
            string mapfile = GlobalVars.MapsDir + @"\\" + GlobalVars.Map;
            string rbxexe = "";
            
                rbxexe = GlobalVars.ClientDir + @"\" + GlobalVars.SelectedClient + @"\Studio" +@"\RobloxStudioBeta.exe";
            
            string quote = "\"";
            string args = "";
            string contents = File.ReadAllText(GlobalVars.ScriptsDir + @"\\" + "studioNothing.lua");
            ReadClientValues(GlobalVars.SelectedClient);
            args = "-script \"loadstring('" + contents + "') " + quote + " " + quote + mapfile + quote;
            try
            {
                Process client = new Process();
                client.StartInfo.FileName = rbxexe;
                client.StartInfo.Arguments = args;
                client.EnableRaisingEvents = true;
                ReadClientValues(GlobalVars.SelectedClient);
                MessageBox.Show(rbxexe, "Lemonaide Launcher - debug", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GlobalVars.presence.largeImageKey = GlobalVars.imagekey_large;
                GlobalVars.presence.state = "In " + GlobalVars.SelectedClient + " Studio";
                GlobalVars.presence.largeImageText = "Lemonaide Client Launcher";
                DiscordRpc.UpdatePresence(ref GlobalVars.presence);
            }
            catch (Exception ex)
            {
                DialogResult result2 = MessageBox.Show("Failed to launch Lemonaide. (Error: " + ex.Message + ")", "Lemonaide Launcher - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        // load the client path and discription thingy
        void ReadClientValues(string ClientName)
        {
            string clientpath = GlobalVars.ClientDir + @"//" + ClientName + @"//clientinfo.txt";

            if (!File.Exists(clientpath))
            {
                MessageBox.Show("No clientinfo.txt detected with the client you chose. The client either cannot be loaded, or it is not available.", "Lemonaide Launcher - Error while loading client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                GlobalVars.SelectedClient = "2016";
            }

            LauncherFuncs.ReadClientValues(clientpath);

           
            // use clientinfo desc rather than labels

            textBox6.Text = GlobalVars.SelectedClientDesc;
            label26.Text = GlobalVars.SelectedClient;
        }
        void ListBox2SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalVars.SelectedClient = listBox2.SelectedItem.ToString();
            ReadClientValues(GlobalVars.SelectedClient);
        }

        //stuff for the tabs, used to tell you what maps are there and your saved ports and IP's
        //very handy to have so you dont need notepad or sticky notes

        void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("SDK NOT IMPLMENTED...", "Lemonaide Launcher - Client Developer SDK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Process.Start("SDK.exe");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartStudio();
            MessageBox.Show("Studio is now loading your chosen file...", "Lemonaide Launcher - Studio", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
