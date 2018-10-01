﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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

}
