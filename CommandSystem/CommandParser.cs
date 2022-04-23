using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord.WebSocket;
using EnBot.CommandSystem.Commands;

namespace EnBot.CommandSystem {
    public static class CommandParser {
        public enum Language {
            English,
            Russian,
        }
        public static Language CurrentLanguage = Language.Russian;
        public static string Prefix = "!";
        /**
         * <summary>List of top level commands to process</summary>
         */
        public static List<ICommand> TopLevelCommands = new List<ICommand>() {
            new Help(),
            new Who(),
            new Ping(),
            new RandomCluster(),
            new EnDungeonsCluster(),
#if DEBUG
            new TestCluster(),
#endif
        };
        /**
         * <summary>Parsing string command</summary>
         * <returns>Did the command parsed?</returns>
         */
        public static bool Parse(string commandRaw, SocketMessage message, BotLogger logger) {
            // Проверка текста на наличие префикса
            if (!commandRaw.StartsWith(Prefix)) return false;
            commandRaw = commandRaw.Remove(0, Prefix.Length);
            // Разбиение команд по словам
            List<string> args = new List<string>(
                commandRaw.Split(' ')
                    .Select(word => word.Trim())
                    .Where(word => word.Length != 0)
            );
            // Если команда пустая
            if (args.Count == 0) return false;
            // Является ли команда запросом на получение информации по команде
            bool isCommandHelpRequest = false;
            if (args.Count != 0) {
                var lastArg = args.Last();
                isCommandHelpRequest = lastArg.EndsWith('?');
                if (isCommandHelpRequest)
                    args[args.Count - 1] = lastArg.Remove(lastArg.Length - 1);
            }
            // Нахождение и выполнение команды
            List<string> leftArgs = null;
            var keyword = args[0];
            args.RemoveAt(0);
            // Поиск нужной команды
            foreach (var topLevelCommand in TopLevelCommands) {
                if (topLevelCommand.Keyword != keyword) continue;
                // Парсинг команды
                var command = topLevelCommand.Parse(args, out leftArgs);
                if (command == null || !command.ArgumentCheck(args, message))
                    continue;
                // Отправка информации о команде
                if (isCommandHelpRequest)
                    message.Channel.SendMessageAsync(command.GetHelpInfo()).GetAwaiter().GetResult();
                // Запуск команды
                else command.Execute(leftArgs, message, logger);
                return true;
            }
            // Если команда не найдена
            return false;
        }
    }
}
