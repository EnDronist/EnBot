using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EnBot.CommandSystem.LocalizationDictionary;

namespace EnBot.CommandSystem.Commands.Test {
    public class MessageWithButtons : Command {
        /**
         * <summary>Локализация</summary>
         * */
        private static LocalizationDictionary _localization = new LocalizationDictionary(
            new LocalizationDictionaryBase {
                { "sends message with Discord-buttons.",
                    new LocalizationDictionaryEntry {
                        { CommandParser.Language.English, args => "sends message with Discord-buttons." },
                        { CommandParser.Language.Russian, args => "отправляет сообщения с Discord-кнопками." },
                    }
                },
            }
        );
        /**
         * <summary>Локализация</summary>
         * */
        public new LocalizationDictionary Localization => _localization;
        public override string Keyword => "message-with-buttons";
        public override string Description => Localization.Get("sends message with Discord-buttons.");
        public override IReadOnlyList<(string/*command name*/, string/*description*/)> ArgumentDescriptions => null;
        /**
         * <summary>Валидация аргументов команды</summary>
         */
        public override bool ArgumentCheck(List<string> args, SocketMessage message)
            => true;
        /**
        * <summary>Запуск команды</summary>
        * <param name="args">Аргументы команды</param>
        * <param name="message">Объект взаимодействия</param>
        * */
        public override void Execute(List<string> args, SocketMessage message, BotLogger logger) {
            //var embedBuilder = new EmbedBuilder() {
            //    Title = "Message with Discord-buttons",
            //    Fields = new List<EmbedFieldBuilder>() { [
            //        Name = "Name",
            //        Value = "{\"content\": \"This is a message with components\", \"components\": [ { \"type\": 1, \"components\": [] } ] }",
            //    ]},
            //};
            //embedBuilder.AddField("hent", "value");
            //Embed embed = embedBuilder.Build();
            message.Channel.SendMessageAsync("Message with Discord-buttons", false, null);
        }
    }
}
