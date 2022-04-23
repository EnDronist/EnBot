using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EnBot.InterventionSystem.Interveners {
    class HelloIntervener : IIntervener {
        private readonly List<Regex> Keywords = new List<Regex>() {
            // Russian
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*((всем|категорический?)\s)*(ку+)+(ру(за|(з|с)ики|(ку+)*))?\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*(всем\s)*(йо+|ё+)у+\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*((все(м|х)|категорически(й|е)?)\s)*пр?и+(в+|ф+)(ки)?(е+т((и|е)ки?|ул(и|е(чки|ньки))|ствую+)?)?\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*(я\s)?((вас|всех|категорически)\s)*пр?и+(в+|ф+)е+тствую+\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*((все(м|х)|категорический?)\s)*х(а+|е+|э+)лл?оу?\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*((всем|категорические?)\s)*(хаю-?)?ха(й|ю|юш(ки|еньки))\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*(всем\s)*з?д(а|о)ров(а+|еньки( булы)?)\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*з?дра+(вст)?в?у+(й|ти+)\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*здравия(\s+желаю)?\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*тевирп\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*(всем\s+)*добр(енький|ый|ющ(еньк)?ий)\s+(ден(ь|ёк|ёчек)|вечер(о((че)?)к?)?)\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*(всем\s+)*добр((еньк)?ого|юще(нько)?го)\s+(утр(а|е(чка|ца))|д(ня|ен(ька|ёчка))|вечер(к?а|очка))\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*(всем\s+)*добр((еньк)?ое|юще(нько)?е)\s+утр(о|е(чко|цо))\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*(всем\s+)*добр((еньк)?ого|юще(нько)?го)\s+времени\s+суток\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*(всем\s+)*утр(а|е(чка|ца))\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*добр(о|е)\sпожаловать\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*нихао\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            // English
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*q+(\s+every(one|(wo)?man|pony))?\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*(hi+)+(-hi+)*(\s+every(one|(wo)?man|pony))?\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*(k(y|u)+)+\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*h(a+|e+)llo\s+(every(one|(wo)?man|pony))?\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*pr(i|e)(v|w)e(t|d)\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*good\s+morning\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*good\s+day\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
            new Regex(@"^(\s*\<\@\!\d*\>\s*)*good\s+evening\.*(\s*\<\@\!\d*\>\s*)*$", RegexOptions.IgnoreCase),
        };
        public void Execute(SocketMessage message, BotLogger logger) {
            foreach (var keyword in Keywords) {
                if (keyword.IsMatch(message.Content))
                    message.AddReactionAsync(PreloadedSources.GuildEmotes["CatDrink"]);
            }
        }
    }   
}