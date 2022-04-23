using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using EnBot.InterventionSystem.Interveners;

namespace EnBot.InterventionSystem {
    public static class InterventionSystem {
        public static readonly List<IIntervener> Interveners = new List<IIntervener>() {
            new HelloIntervener(),
            new GoodbyeIntervener(),
            new BotPingIntervener(),
        };
        public static void Check(SocketMessage message, BotLogger logger) {
            foreach (var intervener in Interveners) {
                intervener.Execute(message, logger);
            }
        }
    }
}