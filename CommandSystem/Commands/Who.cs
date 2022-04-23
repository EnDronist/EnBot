﻿using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace EnBot.CommandSystem.Commands {
    public class Who : Command {
        private static IReadOnlyList<(string, string, string, string)> Prefixes = new List<(string, string, string, string)> {
            ("Хитр", "ый", "ая", "ое"),
            ("Могуч", "ий", "ая", "ее"),
            ("Неожиданн", "ый", "ая", "ое"),
            ("Натуральн", "ый", "ая", "ое"),
            ("Запыханн", "ый", "ая", "ое"),
            ("Грязн", "ый", "ая", "ое"),
            ("Потрёпанн", "ый", "ая", "ое"),
            ("Еле жив", "ой", "ая", "ое"),
            ("Ненадёжн", "ый", "ая", "ое"),
            ("Грациозн", "ый", "ая", "ое"),
            ("Задумчив", "ый", "ая", "ое"),
            ("Чист", "ый", "ая", "ое"),
            ("Очищенн", "ый", "ая", "ое"),
            ("Хорош", "ий", "ая", "ее"),
            ("Плох", "ой", "ая", "ое"),
            ("Красив", "ый", "ая", "ое"),
            ("Шаловлив", "ый", "ая", "ое"),
            ("Мудрён", "ый", "ая", "ое"),
            ("Кучеряв", "ый", "ая", "ое"),
            ("Прилизанн", "ый", "ая", "ое"),
            ("Характерн", "ый", "ая", "ое"),
            ("Угрюм", "ый", "ая", "ое"),
            ("Сонн", "ый", "ая", "ое"),
            ("Небрежн", "ый", "ая", "ое"),
            ("Колюч", "ий", "ая", "ее"),
            ("Гладк", "ий", "ая", "ое"),
            ("Неотёсанн", "ый", "ая", "ое"),
            ("Энергичн", "ый", "ая", "ое"),
            ("Безукоризненн", "ый", "ая", "ое"),
            ("Маленьк", "ий", "ая", "ое"),
            ("Несущественн", "ый", "ая", "ое"),
            ("Микроскопическ", "ий", "ая", "ое"),
            ("Больш", "ой", "ая", "ое"),
            ("Грандиозн", "ый", "ая", "ое"),
            ("Адекватн", "ый", "ая", "ое"),
            ("Неадекватн", "ый", "ая", "ое"),
            ("Упорот", "ый", "ая", "ое"),
            ("Вменяем", "ый", "ая", "ое"),
            ("Интуитивн", "ый", "ая", "ое"),
            ("Кристаллическ", "ий", "ая", "ое"),
            ("Стар", "ый", "ая", "ое"),
            ("Молод", "ой", "ая", "ое"),
            ("Пожил", "ой", "ая", "ое"),
            ("Мистическ", "ий", "ая", "ое"),
            ("Выживш", "ий", "ая", "ее"),
            ("Чахл", "ый", "ая", "ое"),
            ("Угробленн", "ый", "ая", "ое"),
            ("Красн", "ый", "ая", "ое"),
            ("Оранжев", "ый", "ая", "ое"),
            ("Жёлт", "ый", "ая", "ое"),
            ("Зелён", "ый", "ая", "ое"),
            ("Голуб", "ой", "ая", "ое"),
            ("Син", "ий", "яя", "ее"),
            ("Фиолетов", "ый", "ая", "ое"),
            ("Хлипк", "ий", "ая", "ое"),
            ("Ёкарн", "ый", "ая", "ое"),
        };
        private static IReadOnlyList<string> SubjectsMale = new List<string> {
            "торч",
            "торчок",
            "мужик",
            "мужичок",
            "старик",
            "старичок",
            "простак",
            "простачок",
            "дурак",
            "дурачок",
            "чуча",
            "красавчик",
            "натурал",
            "неадекват",
            "одеколон",
            "пузырь",
            "евнух",
            "рыцарь",
            "воин",
            "трус",
            "вор",
            "ворюга",
            "воришка",
            "шалун",
            "шалунишка",
            "мигрант",
            "сибирец",
            "россиянин",
            "украинец",
            "белорус",
            "индиец",
            "американец",
            "итальянец",
            "немец",
            "грек",
            "хворост",
            "дерижабль",
            "дуб",
            "тростник",
            "сахарок",
            "хитрюга",
            "кальмар",
            "краб",
            "осёл",
            "медведь",
            "муравей",
            "муравейник",
            "закат",
            "рассвет",
            "малыш",
            "скромник",
            "торговец",
            "скамер",
            "спамер",
            "наёмник",
            "стрелок",
            "мечник",
            "лучник",
            "изгнанник",
            "танцор",
            "эрудит",
            "диван",
            "стул",
            "компот",
            "веник",
            "венегрет",
            "салат",
            "вояка",
            "пьяница",
            "бабай",
            "незнакомец",
            "столб",
            "цитрус",
            "ультиматум",
            "ритуал",
            "кофе",
            "кумир",
            "щавель",
            "хентай",
            "финал",
            "философ",
            "функционал",
            "фендом",
            "пилот",
            "танкист",
            "игрок",
            "дотер",
            "FNFер",
            "фанат",
            "фанатик",
            "завистник",
            "силуэт",
            "навигатор",
            "президент",
            "претендент",
        };
        private static IReadOnlyList<string> SubjectsFemale = new List<string> {
            "штора",
            "шторка",
            "тростинка",
            "подошва",
            "юбка",
            "банка",
            "чашка",
            "кружка",
            "неадекватка",
            "куртизанка",
            "цапля",
            "украинка",
            "ульта",
            "красота",
            "кузница",
            "котфетка",
            "конфета",
            "ель",
            "нычка",
            "норма",
            "гречка",
            "голубика",
            "гиря",
            "шапка",
            "шляпа",
            "шляпка",
            "зависть",
            "функция",
            "француженка",
            "немка",
            "жопа",
            "жопка",
            "мистика",
            "кура",
            "шкура",
            "курва",
            "чуча",
            "обнова",
            "дотерша",
            "чаща",
            "яндерка",
            "цундерка",
            "яндере",
            "цундере",
            "рыба",
            "рыбка",
            "ладошка",
            "претендентка",
            "статуэтка",
            "бочка",
            "ночь",
            "голова",
            "тварь",
        };
        private static IReadOnlyList<string> SubjectsUndefined = new List<string> {
            "кимоно",
            "конфети",
            "аниме",
            "MLP",
            "существо",
        };
        private static IReadOnlyList<string> Suffixes = new List<string> {
            "",
            "говна",
            "удачи",
            ", но это не точно",
            "без потерь",
            "со смыслом",
            "без смысла",
            "без мозгов",
            "без IQ",
            "без смысла существования",
            "с помидором",
            "с огурцом",
            "с умом",
            "со всем",
            "со всем, что надо",
            "без телефона",
            "без денег",
            "с деньгами",
            "с целым состоянием",
            "с желанием",
            "куда ни шло",
            "с кактусом",
            "с вилкой",
            "с ложкой",
            "без вилки",
            "без ложки",
            "без дошика",
            "без еды",
            "без воды",
            "не без урода",
            "сына собаки",
            "настоящей мужественности",
            "с разбитым носом",
            "ламповости",
            "угнетения",
            "раздора",
            "гнева",
            "поноса",
            "ностальгии",
            "жизни",
            "красоты",
            "разрушения",
            "искупления",
            "покаяния",
            "причастности",
            "упокоения",
            "жалости",
            "липкости",
            "трезвости",
            "покоя",
            "веселья",
            "шаловливости",
            "задора",
            "энергичности",
            "уродливости",
            "с большим сердцем",
            "с боязнью пауков",
            "без страха смерти",
            "с пожилым дедом",
            "в доме",
            "в сортире",
            "в ванне",
            "в кровати",
            "у компа",
            "у компьютера",
            "у сына маминой подруги",
            "в магазе",
            "в супермаркете",
            "на природе",
            "в гостях",
            "на прогулке",
            "на похоронах",
            "в автобусе",
            "в машине",
            "на военной базе",
            "на энергостанции",
            "на хорошей свадьбе",
            "на поминках",
            "на стриме",
            "в чате",
            "без трусов",
            "на морозе",
            "на море",
            "без шапки",
            "с пакетом",
            "со снюсом",
            "с приколом",
            "под землёй",
            "над землёй",
            "в полёте",
            "на самолёте",
            "в майнкрафте",
            "сухости",
            "мудрости",
            "свирепости",
            "чёрного цвеа",
            "синего цвета",
            "красного цвета",
            "белого цвета",
            "фиолетового цвета",
            "оранжевого цвета",
            "жёлтого цвета",
            "зелёного цвета",
            "голубого цвета",
            "серого цвета",
        };
        private static IReadOnlyList<IReadOnlyList<string>> GenderList = new List<IReadOnlyList<string>>() {
            SubjectsMale, SubjectsFemale, SubjectsUndefined
        };
        private static Timer timer = null;
        private void OnTimerElapsed(object sender, ElapsedEventArgs e) {
            timer.Dispose();
            timer = null;
        }
        public override string Keyword => "who";
        public override string Description => "узнай, кто ты на самом деле";
        public override IReadOnlyList<(string, string)> ArgumentDescriptions
            => null;
        public override bool ArgumentCheck(List<string> args, SocketMessage message)
            => true;
        public override void Execute(List<string> args, SocketMessage message, BotLogger logger) {
            // Checking timer
            if (timer != null) return;
            var random = new System.Random();
            var subjectNumber = random.Next(0, SubjectsMale.Count + SubjectsFemale.Count + SubjectsUndefined.Count);
            var genderNumber = subjectNumber < SubjectsMale.Count ? 0
                : subjectNumber < SubjectsMale.Count + SubjectsFemale.Count ? 1
                : 2;
            var subjects = GenderList[genderNumber];
            var randomPrefix = Prefixes[random.Next(0, Prefixes.Count)];
            var randomPrefixAdditions = new List<string>() {
                randomPrefix.Item2, randomPrefix.Item3, randomPrefix.Item4
            };
            var randomSubject = subjects[random.Next(0, subjects.Count)];
            var randomSuffix = Suffixes[random.Next(0, Suffixes.Count)];
            var mention = message.MentionedUsers.FirstOrDefault();
            if (mention != null) 
                message.Channel.SendMessageAsync($"{mention.Mention} -> " +
                    $"{randomPrefix.Item1}{randomPrefixAdditions[genderNumber]} " +
                    $"{randomSubject} {randomSuffix}");
            // Starting timer
            timer = new Timer(10 * 1000);
            timer.Elapsed += OnTimerElapsed;
            timer.Start();
        }
    }
}