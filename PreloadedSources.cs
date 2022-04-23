using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBot {
    public static class PreloadedSources {
        public static readonly Dictionary<string, IEmote> StandardEmotes = new Dictionary<string, IEmote>() {
            { "face_with_raised_eyebrow", new Emoji("\U0001F928"/*🤨*/) },
            { "wave", new Emoji("\U0001F44B"/*👋*/) },
            // Candyrandom
            { "lollipop", new Emoji("\U0001F36D"/*🍭*/) },
            { "candy", new Emoji("\U0001F36C"/*🍬*/) },
        };
        public static readonly Dictionary<string, IEmote> GuildEmotes = new Dictionary<string, IEmote>();
        private static readonly Dictionary<ulong, string> GuildEmoteMap = new Dictionary<ulong, string>() {
            { 864817695262638080, "CatDrink" },
            { 864822591793004564, "KEKW" },
            // Candyrandom
            { 867703069775888394, "Twix" },
            { 867703359430983681, "CandyFirst" },
            { 867703417421561926, "CandySecond" },
            { 867703479521902612, "CandyThird" },
            { 867703546723172382, "CandyFourth" },
            { 867703599140438056, "CandyFifth" },
        };
        public static void Preload(DiscordSocketClient client) {
            var guild = client.GetGuild(864779269300158474);
            var emotes = guild.GetEmotesAsync().Result;
            foreach (var emote in emotes) {
                if (GuildEmoteMap.ContainsKey(emote.Id)) {
                    GuildEmotes.Add(GuildEmoteMap[emote.Id], emote);
                    GuildEmoteMap.Remove(emote.Id);
                }
            }
        }
    }
}