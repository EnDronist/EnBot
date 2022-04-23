using EnBot.EnDungeons.FieldGenerators;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBot.EnDungeons {
    public abstract class Cell {
        public Point Position { get; private set; }
        public Chamber Chamber { get; set; } = null;
        public Cell(Point position) {
            Position = position;
        }
        /*
        public enum Type {
            Wall,
            Floor,
            Fluid,
            Other,
        }
        public Type CellType { get; private set; }
        */
    }
}
