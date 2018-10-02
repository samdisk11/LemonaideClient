using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lemonaide_Client
{
    public static class GlobalVars
    {
        public static string ClientDir = "";
        public static string ScriptsDir = "";
        public static string MapsDir = "";
        public static string CustomPlayerDir = "";
        public static string IP = "localhost";
        public static string Version = "";
        public static string SharedArgs = "";
        // server settings
        public static string Map = "Baseplate.rbxl";
        public static int RobloxPort = 53640;
        public static int ServerPort = 53640;
        public static int DefaultRobloxPort = 53640;
        public static int PlayerLimit = 12;
        public static int RespawnTime = 5;
        public static DiscordRpc.RichPresence presence;
        public static string appid = "495748400151527424";
        public static string imagekey_large = "lemon";
        // clientinfo creator

        public static string ClientCreator_SelectedClientDesc = "";

        // client shit

        public static string SelectedClient = "";
        public static string SelectedClientDesc = "";
        public static string SelectedClientMD5 = "";
    }

    //Discord Rich Presence Integration :D
    public class DiscordRpc
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ReadyCallback();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void DisconnectedCallback(int errorCode, string message);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ErrorCallback(int errorCode, string message);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void JoinCallback(string secret);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SpectateCallback(string secret);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void RequestCallback(JoinRequest request);

        public struct EventHandlers
        {
            public ReadyCallback readyCallback;
            public DisconnectedCallback disconnectedCallback;
            public ErrorCallback errorCallback;
            public JoinCallback joinCallback;
            public SpectateCallback spectateCallback;
            public RequestCallback requestCallback;
        }

        [System.Serializable]
        public struct RichPresence
        {
            public string state; /* max 128 bytes */
            public string details; /* max 128 bytes */
            public long startTimestamp;
            public long endTimestamp;
            public string largeImageKey; /* max 32 bytes */
            public string largeImageText; /* max 128 bytes */
            public string smallImageKey; /* max 32 bytes */
            public string smallImageText; /* max 128 bytes */
            public string partyId; /* max 128 bytes */
            public int partySize;
            public int partyMax;
            public string matchSecret; /* max 128 bytes */
            public string joinSecret; /* max 128 bytes */
            public string spectateSecret; /* max 128 bytes */
            public bool instance;
        }

        [System.Serializable]
        public struct JoinRequest
        {
            public string userId;
            public string username;
            public string avatar;
        }

        public enum Reply
        {
            No = 0,
            Yes = 1,
            Ignore = 2
        }

        [DllImport("discord-rpc", EntryPoint = "Discord_Initialize", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Initialize(string applicationId, ref EventHandlers handlers, bool autoRegister, string optionalSteamId);

        [DllImport("discord-rpc", EntryPoint = "Discord_Shutdown", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Shutdown();

        [DllImport("discord-rpc", EntryPoint = "Discord_RunCallbacks", CallingConvention = CallingConvention.Cdecl)]
        public static extern void RunCallbacks();

        [DllImport("discord-rpc", EntryPoint = "Discord_UpdatePresence", CallingConvention = CallingConvention.Cdecl)]
        public static extern void UpdatePresence(ref RichPresence presence);

        [DllImport("discord-rpc", EntryPoint = "Discord_Respond", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Respond(string userId, Reply reply);
    }
    public class LauncherFuncs
    {
        public static void ReadClientValues(string clientpath)
        {
            string line1;
            string Decryptline1, Decryptline2, Decryptline3, Decryptline4, Decryptline5, Decryptline6, Decryptline7, Decryptline8;

            using (StreamReader reader = new StreamReader(clientpath))
            {
                line1 = reader.ReadLine();
            }

            if (!SecurityFuncs.IsBase64String(line1))
                return;

            string ConvertedLine = SecurityFuncs.Base64Decode(line1);
            string[] result = ConvertedLine.Split('|');
            Decryptline1 = SecurityFuncs.Base64Decode(result[0]);
            Decryptline2 = SecurityFuncs.Base64Decode(result[1]);
            Decryptline3 = SecurityFuncs.Base64Decode(result[2]);
            Decryptline4 = SecurityFuncs.Base64Decode(result[3]);
            Decryptline5 = SecurityFuncs.Base64Decode(result[4]);
            Decryptline6 = SecurityFuncs.Base64Decode(result[5]);
            Decryptline7 = SecurityFuncs.Base64Decode(result[6]);
            Decryptline8 = SecurityFuncs.Base64Decode(result[7]);



            GlobalVars.SelectedClientDesc = Decryptline8;


        }

        public static void ReadClientValuesBCC(string ClientName)
        {
            string clientpath = GlobalVars.ClientDir + @"\\" + ClientName + @"\\clientinfo.txt";

            if (!File.Exists(clientpath))
            {
                GlobalVars.SelectedClient = "2016";
            }

            ReadClientValues(clientpath);
        }
    }
    public class SecurityFuncs
    {
        public SecurityFuncs()
        {
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static bool IsBase64String(string s)
        {
            s = s.Trim();
            return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }

    }

   
}
