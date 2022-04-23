using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using static EnBot.CommandSystem.LocalizationDictionary;

namespace EnBot.CommandSystem.Commands.Random {
    public class CandyRandom : Command {
        static readonly List<IEmote> Emotes = new List<IEmote>() {
            PreloadedSources.StandardEmotes["lollipop"],
            PreloadedSources.StandardEmotes["candy"],
            PreloadedSources.GuildEmotes["Twix"],
            PreloadedSources.GuildEmotes["CandyFirst"],
            PreloadedSources.GuildEmotes["CandySecond"],
            PreloadedSources.GuildEmotes["CandyThird"],
            PreloadedSources.GuildEmotes["CandyFourth"],
            PreloadedSources.GuildEmotes["CandyFifth"],
        };
        /**
         * <summary>Локализация</summary>
         * */
        private static LocalizationDictionary _localization = new LocalizationDictionary(
            new LocalizationDictionaryBase {
                { "gives you a random candy.",
                    new LocalizationDictionaryEntry {
                        { CommandParser.Language.English, args => "gives you a random candy." },
                        { CommandParser.Language.Russian, args => "выдаёт тебе случайную конфетку." },
                    }
                },
            }
        );
        /**
         * <summary>Локализация</summary>
         * */
        public new LocalizationDictionary Localization => _localization;
        public override string Keyword => "candy-random";
        public override string Description => Localization.Get("gives you a random candy.");
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
            var emote = Emotes[new System.Random().Next(Emotes.Count)];
            message.AddReactionAsync(emote).GetAwaiter().GetResult();
            logger.LogReactionAdded(message, emote);
        }
    }
}