using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBot.EnDungeons.Entities.Creatures {
    public class PlayerEntity : Entity {
        public Player Player { get; private set; }
        public PlayerEntity(Player player, Field field, Point position) : base(field, position) {
            Player = player;
        }
    }
}
