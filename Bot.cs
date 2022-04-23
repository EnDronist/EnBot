using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnDronist.Support;
using EnBot.CommandSystem;
using EnBot.InterventionSystem;
using EnBot.EnBotJsAPI;
using System.Net;
using Newtonsoft.Json;

namespace EnBot {
    public class Bot {
        public static readonly DiscordSocketClient Client = new DiscordSocketClient();
        public static readonly HttpListener JsHelperClient = new HttpListener();
        public static readonly BotLogger Logger = new BotLogger();
        public static string Token { get; private set; }
        public static async Task HandleJsHelperConnection() {
            while (true) {
                HttpListenerContext context = await JsHelperClient.GetContextAsync();
                Console.WriteLine("JsHelperClient: ".Blue() + "\"clickButton\" server event activated.");
                if (context.Request.HttpMethod != "POST") {
                    Console.WriteLine("JsHelperClient: ".Blue() + "http method is not \"POST\"".Red());
                    continue;
                }
                // Parsing js helper message
                if (!context.Request.HasEntityBody) {
                    Console.WriteLine("JsHelperClient: ".Blue() + "request has no body.".Red());
                    continue;
                }
                Stream body = context.Request.InputStream;
                Encoding encoding = context.Request.ContentEncoding;
                StreamReader reader = new StreamReader(body, encoding);
                var bodyString = reader.ReadToEnd();
                body.Close(); reader.Close();
                Console.WriteLine("HandleJsHelperConnection: ".Yellow() + bodyString);
                //var jsHelperMessage = JsonConvert.DeserializeObject<JsHelperMessage>();
                // Parsing js helper message by command parser
                //Logger.LogJsHelperMessageReceive();
                //bool isCommandParsed = false;
                //try {
                    // Parsing and executing command
                //    isCommandParsed = CommandParser.Parse(socketMessage.Content, socketMessage, Logger);
                    // Intervention check
                //    if (!isCommandParsed) {
                //        InterventionSystem.InterventionSystem.Check(socketMessage, Logger);
                //    }
                //}
                //catch (Exception e) {
                //    Logger.LogError(e);
                //}
            }
        }
        public async Task RunAsync() {
            // Configuration
            Client.MessageReceived += OnMessageReceived;
            Client.Log += OnLog;
            Client.Ready += OnReady;
            // Getting login token
            var token = GetToken();
            if (token == null) {
                Logger.LogError(new Exception("Token not found"));
                return;
            }
            Token = token;
            // Loginning
            await Client.LoginAsync(TokenType.Bot, GetToken());

            // Configuring js helper http listener (Сейчас проблема тут)
            JsHelperClient.Prefixes.Add("https://localhost:3001/");
            JsHelperClient.Prefixes.Add("https://localhost:3002/");
            JsHelperClient.Prefixes.Add("https://localhost:3003/");
            Task listenTask = HandleJsHelperConnection();

            // Configuring js helper server info
            ServerAPI.IpAddress = "localhost";
            ServerAPI.Port = 3002;

            // Starting bot
            await Client.StartAsync();
        }
        private Task OnReady() {
            PreloadedSources.Preload(Client);
            return Task.CompletedTask;
        }
        private Task OnLog(LogMessage message) {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }
        private Task OnMessageReceived(SocketMessage socketMessage) {
            // Bot message check
            if (socketMessage.Author.IsBot) {
                Logger.LogMessageResponse(socketMessage);
                return Task.CompletedTask;
            }
            // Logging
            Logger.LogMessageReceive(socketMessage);
            bool isCommandParsed = false;
            try {
                // Parsing and executing command
                isCommandParsed = CommandParser.Parse(socketMessage.Content, socketMessage, Logger);
                // Intervention check
                if (!isCommandParsed) {
                    InterventionSystem.InterventionSystem.Check(socketMessage, Logger);
                }
            }
            catch (Exception e) {
                Logger.LogError(e);
            }
            // End
            return Task.CompletedTask;
        }
        private string GetToken() {
            try {
                return File.ReadAllText("./Info/token.txt");
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            return null;
        }
    }
}