using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBot.InterventionSystem {
    public interface IIntervener {
        public abstract void Execute(SocketMessage message, BotLogger logger);
    }
}
