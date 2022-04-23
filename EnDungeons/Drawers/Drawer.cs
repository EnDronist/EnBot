using Discord;
using EnBot.EnDungeons.Entities.Creatures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBot.EnDungeons.Drawers {
    public abstract class Drawer {
        /**
         * <summary>Textures associated with cells type</summary>
         */
        public abstract IReadOnlyDictionary<Type, string> Textures { get; }
        public abstract Point DrawingWindow { get; }
        public abstract string Draw(Field field, PlayerEntity playerEntity);
    }
}
