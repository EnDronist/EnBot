using Discord.Rest;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBot.CommandSystem.Commands {
    public class Ping : Command {
        public override string Keyword => "ping";
        public override string Description => "проверить пинг бота";
        public override IReadOnlyList<(string, string)> ArgumentDescriptions
            => null;
        public override bool ArgumentCheck(List<string> args, SocketMessage message)
            => true;
        public override void Execute(List<string> args, SocketMessage message, BotLogger logger) {
            var timeDelay = DateTime.Now - message.Timestamp;
            var response = message.Channel.SendMessageAsync($"Задержка начала выполнения команды: {(int)timeDelay.TotalMilliseconds}мс").Result;
            timeDelay = DateTime.Now - response.Timestamp;
            var secondResponse = message.Channel.SendMessageAsync($"Рассинхронизация во времени между ботом и сервером: {(int)timeDelay.TotalMilliseconds}мс").Result;
            timeDelay = secondResponse.Timestamp - response.Timestamp;
            message.Channel.SendMessageAsync($"Задержка между ботом и сервером: {(int)(timeDelay.TotalMilliseconds / 3.0)}мс");
        }
    }
}
