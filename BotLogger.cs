using Discord;
using Discord.WebSocket;
using EnDronist.Support;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBot {
    public class BotLogger {
        private string GetGuildName(SocketMessage socketMessage) {
            string guildName = null;
            try {
                guildName = ((SocketGuildChannel)socketMessage.Channel)?.Guild.Name;
            }
            catch { }
            if (guildName == null) guildName = "PM";
            return guildName;
        }
        public void Log(string message, bool debug = false) {
            (
                DateTime.Now.ToLocalTime().ToString().Green()
                + " [".DarkGray() + "Log".Blue() + "]".DarkGray()
                + ": ".DarkGray() + message.White()
            ).WriteLine();
        }
        public void LogMessageReceive(SocketMessage socketMessage) {
            var guildName = GetGuildName(socketMessage);
            (
                socketMessage.Timestamp.LocalDateTime.ToString().Green()
                + " [".DarkGray() + guildName.White() + "]".DarkGray()
                + " in ".DarkGray() + socketMessage.Channel.ToString().White()
                + " by ".DarkGray() + socketMessage.Author.ToString().Cyan()
                + ": ".DarkGray() + socketMessage.Content.White()
            ).WriteLine();
        }
        public void LogJsHelperMessageReceive(JsHelperMessage socketMessage) {
            (
                "LogJsHelperMessageReceive KEKW".Green()
                //socketMessage.Timestamp.LocalDateTime.ToString().Green()
                //+ " [".DarkGray() + guildName.White() + "]".DarkGray()
                //+ " in ".DarkGray() + socketMessage.Channel.ToString().White()
                //+ " by ".DarkGray() + socketMessage.Author.ToString().Cyan()
                //+ ": ".DarkGray() + socketMessage.Content.White()
            ).WriteLine();
        }
        public void LogMessageResponse(SocketMessage responseMessage) {
            var guildName = GetGuildName(responseMessage);
            (
                "Answering =>".DarkGray()
                + " [".DarkGray() + guildName.White() + "]".DarkGray()
                + " in ".DarkGray() + responseMessage.Channel.ToString().White()
                + ": ".DarkGray() + responseMessage.Content.White()
            ).WriteLine();
        }
        public void LogReactionAdded(SocketMessage socketMessage, IEmote emote) {
            var guildName = GetGuildName(socketMessage);
            var bytes = Encoding.UTF8.GetBytes(emote.Name);
            if (bytes.Length > 3)
                bytes = bytes.Where((elem, index) => index != 0).ToArray();
            bytes = Support.Encoding.UTF6.FromUTF8(bytes);
            var emoteText = string.Concat(bytes.Select(elem
                => Convert.ToHexString(new[] { elem }))
            ).ToUpper();
            if (emoteText.StartsWith('0'))
                emoteText = emoteText.Remove(0, 1);
            emoteText = "U+" + emoteText;
            (
                "Added reaction (".DarkGray() + emoteText.White() + ") =>".DarkGray()
                + " [".DarkGray() + guildName.White() + "]".DarkGray()
                + " in ".DarkGray() + socketMessage.Channel.ToString().White()
                + " to ".DarkGray() + socketMessage.Author.ToString().Cyan()
                + "'s message: ".DarkGray() + socketMessage.Content.White()
            ).WriteLine();
        }
        public void LogError(Exception e) {
            $"Error: {e.Message}".Red().WriteLine();
        }
    }
}
