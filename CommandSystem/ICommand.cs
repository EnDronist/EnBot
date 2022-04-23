using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBot.CommandSystem {
    public interface ICommand {
        /**
         * <summary>Локализация</summary>
         * */
        public abstract LocalizationDictionary Localization { get; }
        /**
         * <summary>Список кластеров команд</summary>
         * */
        public abstract IReadOnlyList<ICommand> Subcommands { get; }
        /**
        * <summary>Ключевое слово команды</summary>
        * */
        public abstract string Keyword { get; }
        /**
         * <summary>Описание команды (может иметь значение null)</summary>
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
        public abstract string GetHelpInfo();
        /**
         * <summary>Разбор команды кластером и поиск команды</summary>
         * <param name="args">Аргументы команды</param>
         * <param name="leftArgs">Оставшиеся аргументы команды</param>
         * */
        public ICommand Parse(List<string> args, out List<string> leftArgs);
        /**
         * <summary>Запуск команды</summary>
         * <param name="args">Аргументы команды</param>
         * <param name="message">Объект взаимодействия</param>
         * */
        public abstract void Execute(List<string> args, SocketMessage message, BotLogger logger);
    }
}