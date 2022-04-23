using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Discord.WebSocket;
using EnBot.Support;
using static EnBot.CommandSystem.LocalizationDictionary;

namespace EnBot.CommandSystem {
    public abstract class CommandCluster : ICommand {
        /**
         * <summary>Локализация</summary>
         * */
        private static LocalizationDictionary _localization = new LocalizationDictionary(
            new LocalizationDictionaryBase {
                // args => 0: (string) keyword, 1: (string) description, 2: (int) commandsCount
                { "<ClusterDescription>",
                    new LocalizationDictionaryEntry {
                        { CommandParser.Language.English, args => $"Command cluster \"**{args[0]}**\": "
                            + (args[1] != null ? $"{args[1]}.\n" : "")
                            + $"**{args[2]}** commands total.\n" },
                        { CommandParser.Language.Russian, args => $"Набор команд \"**{args[0]}**\": "
                            + (args[1] != null ? $"{args[1]}.\n" : "")
                            + $"**{args[2]}** команд{((int)args[2]).RusCase("а", "ы", "")} всего.\n" },
                    }
                },
                { "Commands",
                    new LocalizationDictionaryEntry {
                        { CommandParser.Language.English, args => "Commands" },
                        { CommandParser.Language.Russian, args => "Команды" },
                    }
                },
                { "Write command name with a question mark additionally to see details",
                    new LocalizationDictionaryEntry {
                        { CommandParser.Language.English, args => "Write command name with a question mark additionally to see details" },
                        { CommandParser.Language.Russian, args => "Напишите дополнительно название команды с вопросительным знаком для подробностей" },
                    }
                },
            }
        );
        /**
         * <summary>Локализация</summary>
         * */
        public LocalizationDictionary Localization => _localization;
        /**
         * <summary>Список доступных команд в кластере</summary>
         * */
        public abstract IReadOnlyList<ICommand> Subcommands { get; }
        /**
         * <summary>Ключевое слово кластера</summary>
         * */
        public abstract string Keyword { get; }
        /**
         * <summary>Описание команды</summary>
         * */
        public abstract string Description { get; }
        /**
        * <summary>Список аргументов команды и их описаний</summary>
        * */
        public IReadOnlyList<(string/*command name*/, string/*description*/)> ArgumentDescriptions
            => new List<(string, string)>();
        /**
         * <summary>Валидация аргументов команды</summary>
         */
        public bool ArgumentCheck(List<string> args, SocketMessage message)
            => true;
        /**
         * <summary>Описание состава кластера</summary>
         * */
        public string GetHelpInfo() {
            // Если у кластера нет ключевого слова
            if (Keyword == null) return null;
            // Наполнение описания
            var commands = Subcommands;
            var kek = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());
            string result = Localization.Get("<ClusterDescription>",  new object[] { Keyword, Description, commands.Count });
            if (commands.Count != 0) {
                result += Localization.Get("Commands") + ":\n"
                    + string.Join("\n", Subcommands.Select(command => $"- **{command.Keyword}**")) + "\n";
                result += Localization.Get("Write command name with a question mark additionally to see details") + ".";
            }
            return result.Trim();
        }
        /**
         * <summary>Разбор команды кластером и поиск команды</summary>
         * <param name="args">Аргументы команды</param>
         * <param name="leftArgs">Оставшиеся аргументы команды</param>
         * */
        public ICommand Parse(List<string> args, out List<string> leftArgs) {
            leftArgs = new List<string>(args);
            // Если аргументы отсутствуют
            if (leftArgs.Count == 0) return this;
            // Если аргументы есть
            foreach (var subcommand in Subcommands) {
                if (leftArgs[0] == subcommand.Keyword) {
                    leftArgs.RemoveAt(0);
                    return subcommand.Parse(leftArgs, out leftArgs);
                }
            }
            return null;
        }
        /**
         * <summary>Запуск команды</summary>
         * <param name="args">Аргументы команды</param>
         * <param name="message">Объект взаимодействия</param>
         * */
        public void Execute(List<string> args, SocketMessage message, BotLogger logger) {
            message.Channel.SendMessageAsync(GetHelpInfo());
        }
    }
}