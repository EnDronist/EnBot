using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace EnBot {
    class Program {
        static void Main(string[] args) {
            // Running bot
            var bot = new Bot();
            bot.RunAsync().GetAwaiter().GetResult();
            // End
            Console.ReadLine();
        }
    }
}
