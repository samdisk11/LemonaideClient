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

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // lmao u a hacker
            MessageBox.Show("You found a easter egg! well done..", "Lemonaide Launcher - Easter Egg", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
