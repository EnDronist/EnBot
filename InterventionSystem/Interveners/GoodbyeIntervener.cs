using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EnBot.InterventionSystem.Interveners {
    class GoodbyeIntervener : IIntervener {
        private readonly List<Regex> Keywords = new List<Regex>() {
            // Russian
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*пок(а|еда)(-пок(а|еда))?\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*бб(ш(еч)?ки)?\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*проща(й|юсь)\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*(всем\s+)(д|б)обра\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*до\s+(после)*завтра\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*до\s+((скорой|следующей|(после)*завтрашней)\s+)?встречи\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*до\s+(понедельника|вторника|среды|четверга|пятницы|субботы|воскресенья)\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*до\s*свид(ан(ия|ьки|ки)|ос(ики?)?|ули)\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*ар+и\s*ведерчи\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            // English
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*bb(shk(i|y))?\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*pok(a|eda)\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*(bye)+(-bye)*\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
        };
        public void Execute(SocketMessage message, BotLogger logger) {
            foreach (var keyword in Keywords) {
                if (keyword.IsMatch(message.Content))
                    message.AddReactionAsync(PreloadedSources.StandardEmotes["wave"]);
            }
        }
    }
}
