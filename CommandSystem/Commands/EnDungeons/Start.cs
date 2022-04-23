using Discord.WebSocket;
using EnBot.EnBotJsAPI;
using EnBot.EnBotJsAPI.ServerFunctions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static EnBot.EnBotJsAPI.CreateButtons.Response.Button;
using static EnBot.EnBotJsAPI.ServerFunctionAPI;

namespace EnBot.CommandSystem.Commands.EnDungeons {
    public class Start : Command {
        public override string Keyword => "start";
        public override string Description => "начинает новую игровую сессию.";
        public override IReadOnlyList<(string, string)> ArgumentDescriptions
            => null;
        public override bool ArgumentCheck(List<string> args, SocketMessage message)
            => true;
        public override void Execute(List<string> args, SocketMessage message, BotLogger logger) {
            var sessions = EnBot.EnDungeons.EnDungeonsHandler.Sessions;
            string response;
            // Если игра уже запущена
            if (sessions.ContainsKey(message.Author)) {
                // Ответ пользователю
                response = $"{message.Author.Mention} Игровая сессия уже запущена. Завершите её, чтобы начать новую игру.";
                message.Channel.SendMessageAsync(response).GetAwaiter().GetResult();
                return;
            }
            // Запуск игры
            sessions.Add(message.Author, new EnBot.EnDungeons.EnDungeonsSession(message));
            // Ответ пользователю
            //response = $"{message.Author.Mention} Игровая сессия начата.";
            //message.Channel.SendMessageAsync(response).GetAwaiter().GetResult();
            var session = sessions[message.Author];
            var result = session.Draw();
            //message.Channel.SendMessageAsync(embed).GetAwaiter().GetResult();
            // Preparing http client
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback =
                (sender, cert, chain, sslPolicyErrors) => true;
            var httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = new TimeSpan(0, 0, 5);
            // Sending request to helper server
            var requestQuery = new Authorization.Request(Bot.Token);
            var requestBody = new CreateButtons.Response(message.Channel.Id.ToString(), result, new List<CreateButtons.Response.Button>() {
                new CreateButtons.Response.Button("Yes", Colors.Blurple),
                new CreateButtons.Response.Button("Sure", Colors.Green),
                new CreateButtons.Response.Button("Maybe", Colors.Red),
            });
            var json = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var request = new ResponseData(requestQuery.ToQuery().Get, json);
            try {
                var requestResult = httpClient.PostAsync(request.Get, request.Post).GetAwaiter().GetResult();
                if (requestResult.StatusCode != System.Net.HttpStatusCode.OK) {
                    message.Channel.SendMessageAsync($"EnDungeons JS Helper server error: {requestResult.Content} [{requestResult.StatusCode}]");
                }
                logger.Log($"[{requestResult.StatusCode}] {requestResult.Content}");
            }
            catch {
                message.Channel.SendMessageAsync($"EnDungeons JS Helper server is offline or unavailable.");
            }
            //logger.Log(result.Content.ReadAsStringAsync().GetAwaiter().GetResult(), true);
        }
    }
}