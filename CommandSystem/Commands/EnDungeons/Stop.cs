using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBot.CommandSystem.Commands.EnDungeons {
    public class Stop : Command {
        public override string Keyword => "stop";
        public override string Description => "останавливает текущую игровую сессию.";
        public override IReadOnlyList<(string, string)> ArgumentDescriptions
            => null;
        public override bool ArgumentCheck(List<string> args, SocketMessage message)
            => true;
        public override void Execute(List<string> args, SocketMessage message, BotLogger logger) {
            var sessions = EnBot.EnDungeons.EnDungeonsHandler.Sessions;
            string response;
            // Если игра не запущена
            if (!sessions.ContainsKey(message.Author)) {
                // Ответ пользователю
                response = $"{message.Author.Mention} У вас нет активных игровых сессий.";
                message.Channel.SendMessageAsync(response).GetAwaiter().GetResult();
                return;
            }
            // Остановка игры
            sessions.Remove(message.Author);
            // Ответ пользователю
            response = $"{message.Author.Mention} Игровая сессия завершена.";
            message.Channel.SendMessageAsync(response).GetAwaiter().GetResult();
        }
    }
}
