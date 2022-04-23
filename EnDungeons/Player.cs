using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBot.EnDungeons {
    public class Player {
        public SocketUser User { get; private set; }
        public Player(SocketUser user) {
            User = user;
        }
    }
}
