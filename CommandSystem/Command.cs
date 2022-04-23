using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using static EnBot.CommandSystem.LocalizationDictionary;

namespace EnBot.CommandSystem {
    public abstract class Command : ICommand {
        /**
         * <summary>Локализация</summary>
         * */
        private static LocalizationDictionary _localization = new LocalizationDictionary(
            new LocalizationDictionaryBase {
                { "Command",
                    new LocalizationDictionaryEntry {
                        { CommandParser.Language.English, args => "Command" },
                        { CommandParser.Language.Russian, args => "Команда" },
                    }
                },
                { "Command has no arguments.",
                    new LocalizationDictionaryEntry {
                        { CommandParser.Language.English, args => "Command has no arguments." },
                        { CommandParser.Language.Russian, args => "Команда не имеет аргументов." },
                    }
                },
                { "arguments",
                    new LocalizationDictionaryEntry {
                        { CommandParser.Language.English, args => "arguments" },
                        { CommandParser.Language.Russian, args => "аргументов" },
                    }
                },
            }
        );
        /**
         * <summary>Локализация</summary>
         * */
        public LocalizationDictionary Localization => _localization;
        /**
         * <summary>Список кластеров команд</summary>
         * */
        public IReadOnlyList<ICommand> Subcommands
            => null;
        /**
        * <summary>Ключевое слово команды</summary>
        * */
        public abstract string Keyword { get; }
        /**
         * <summary>Описание команды</summary>
         * */
        public abstract string Description { get; }
        /**
        * <summary>Список аргументов команды и их описаний</summary>
        * */
        public abstract IReadOnlyList<(string/*command name*/, string/*description*/)> ArgumentDescriptions { get; }
        /**
         * <summary>Валидация аргументов команды (true, если не требуется)</summary>
         */
        public abstract bool ArgumentCheck(List<string> args, SocketMessage message);
        /**
         * <summary>Описание команды и её аргументов</summary>
         * */
        public string GetHelpInfo() {
            var result = $"{Localization.Get("Command")} \"**{Keyword}**\": " +
                $"{Description}\n";
            // Если у команды нет аргументов
            if (ArgumentDescriptions == null || ArgumentDescriptions.Count == 0)
                result += $"{Localization.Get("Command has no arguments.")}\n";
            // Если у команды есть аргументы
            else {
                result += $"{ArgumentDescriptions.Count} {Localization.Get("arguments")}:\n";
                foreach (var argumentDescription in ArgumentDescriptions) {
                    result += $"- **{argumentDescription.Item1}**: {argumentDescription.Item2}";
                }
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
            return this;
        }
        /**
         * <summary>Запуск команды</summary>
         * <param name="args">Аргументы команды</param>
         * <param name="message">Объект взаимодействия</param>
         * */
        public abstract void Execute(List<string> args, SocketMessage message, BotLogger logger);
    }
}