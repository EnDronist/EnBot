using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using static EnBot.EnBotJsAPI.ServerFunctionAPI;

namespace EnBot.EnBotJsAPI {
    public class ServerAPI {
        public static string IpAddress { get; set; } = null;
        public static int Port { get; set; } = 3000;
        public static ResponseData QueryFormat(string urn, HttpContent data) {
            if (IpAddress == null) throw new Exception("IP Address is empty.");
            return new ResponseData($"https://{IpAddress}:{Port}/{urn}", data);
        }
    }
}