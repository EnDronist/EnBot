using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBot.EnDungeons {
    public class EnDungeonsHandler {
        public static Dictionary<SocketUser, EnDungeonsSession> Sessions = new Dictionary<SocketUser, EnDungeonsSession>();
    }
}
