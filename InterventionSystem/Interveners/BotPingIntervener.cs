using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;

namespace EnBot.InterventionSystem.Interveners {
    class BotPingIntervener : IIntervener {
        private readonly List<Regex> Keywords = new List<Regex>() {
            new Regex(@"(\<\@\!?864711287267393536\>\s*)+", RegexOptions.IgnoreCase),
        };
        private readonly List<string> Answers = new List<string>() {
            "Шо ннада?",
            "Не трогай мои exalts",
            "Задолбали пинговать",
            "Хватит пинговать",
            "Ща как пингану в ответ",
            "Ещё раз пинганёшь - удалю Discord",
            "Иди поспе",
            "Не трогай свечу",
            "Хорошо пингуешь",
            "Я бы красивей пинганул",
            "Тебе что-то нужно?",
            "Не думай, что я не вижу",
            "OMFG LMAO",
            "Ну ЯЯЯ!",
            "@everyone KEKW",
            "Ой, а что это ты делаешь?",
            "Не, ну это бан",
            "Не, ну это бан, реааально",
            "Ты меня тестируешь?",
            "Я теряю терпение",
            "У меня болит попа. Не знаю в чём причина. Вроде туда ничего не пихал. Мне нужна помощь. Знаю у тестера болела попа, потому что от своего веса он проломил ножки своей табуретке и очень сильно ударился попой. Я видимо тоже как то ударился ею и теперь она у меня болит. Я знаю тестер мне поможет решить эту проблему.",
            "Ой, достал",
            "Ой, всё",
            "Я всё вижу!",
            "Давай, пингани ещё раз, попробуй",
            "Спасибо за пинг :)",
            "Ты мастер пинговать ботов",
            "Я чиллю, не мешай",
            "Шошошо?",
            "https://tenor.com/view/discord-ping-pingus-bingus-cat-gif-18905873",
        };
        private static Timer timer = null;
        public void Execute(SocketMessage message, BotLogger logger) {
            foreach (var keyword in Keywords) {
                if (keyword.IsMatch(message.Content)) {
                    var emote = PreloadedSources.StandardEmotes["face_with_raised_eyebrow"];
                    message.AddReactionAsync(emote);
                    logger.LogReactionAdded(message, emote);
                    Bot.Client.SetActivityAsync(new PingActivity($"на {message.Author.Username}"));
                    if (timer == null) {
                        timer = new Timer(20 * 1000);
                        timer.Elapsed += OnTimerElapsed;
                        timer.Start();
                        int randomInt = new Random().Next(0, Answers.Count);
                        _ = message.Channel.SendMessageAsync($"{message.Author.Mention} {Answers[randomInt]}").Result;
                    }
                }
            }
        }
        private void OnTimerElapsed(object sender, ElapsedEventArgs e) {
            timer.Dispose();
            timer = null;
        }
        class PingActivity : IActivity {
            private string name;
            public PingActivity(string name) {
                this.name = name;
            }
            public string Name => name;

            public ActivityType Type => ActivityType.Watching;

            public ActivityProperties Flags => ActivityProperties.None;

            public string Details => "Details";
        }
    }
}
