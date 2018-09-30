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
using DiscordRPC;
namespace Lemonaide_Client
{
    public partial class MainMenu : Form
    {
        public DiscordRpcClient client;
        void Initialize()
        {
            /*
            Create a discord client
            NOTE: 	If you are using Unity3D, you must use the full constructor and define
                     the pipe connection as DiscordRPC.IO.NativeNamedPipeClient
            */
            client = new DiscordRpcClient(495748400151527424, true, DiscordRPC.IO.NativeNamedPipeClient))					
	
	//Set the logger

            //Connect to the RPC
            client.Initialize();

            //Set the rich presence
            client.SetPresence(new RichPresence()
            {
                Details = "Lemonaide Client",
                State = "In Main Menu",
                Assets = new Assets()
                {
                    LargeImageKey = "lemon",
                    LargeImageText = "Lemonaide Client Launcher",
                    SmallImageKey = "none"
                }
            });
        }

        public MainMenu()
        {
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

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // lmao u a hacker
            MessageBox.Show("You found a easter egg! well done..", "Lemonaide Launcher - Easter Egg", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
