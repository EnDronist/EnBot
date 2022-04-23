using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBot.CommandSystem.Commands {
    public class Help : Command {
        public override string Keyword => "help";
        public override string Description => null;
        public override IReadOnlyList<(string, string)> ArgumentDescriptions
            => null;
        public override bool ArgumentCheck(List<string> args, SocketMessage message)
            => true;
        public override void Execute(List<string> args, SocketMessage message, BotLogger logger) {
            var response = "Список команд:";
            //bool isResponseEmpty = true;
            foreach (var topLevelCommand in CommandParser.TopLevelCommands) {
                // Добавление запятой к строке
                //if (!isResponseEmpty) response += ",\n";
                // Добавление команды или кластера команд и описания
                response += $"\n- **{topLevelCommand.Keyword}**"
                    + (topLevelCommand is CommandCluster ? " (набор команд)" : "")
                    + (topLevelCommand.Description != null ? $": {topLevelCommand.Description}" : "");
                //isResponseEmpty = false;
            }
            message.Channel.SendMessageAsync($"{message.Author.Mention} {response}");
        }
    }
}