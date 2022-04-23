using EnBot.EnDungeons.Cells;
using EnBot.EnDungeons.Entities;
using EnBot.EnDungeons.FieldGenerators;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBot.EnDungeons {
    public class Field {
        public Point Size { get; private set; }
        public Cell[][] Cells { get; private set; } = null;
        public List<Entity> Entities { get; private set; } = new List<Entity>();
        public List<Chamber> Chambers { get; private set; } = new List<Chamber>();
        public Field(Point size) {
            Size = size;
            // Creating field
            Cells = new Cell[size.Y][];
            for (var y = 0; y < size.Y; y++) {
                Cells[y] = new Cell[size.X];
                for (var x = 0; x < size.X; x++) {
                    Cells[y][x] = new Floor(new Point(x, y));
                }
            }
        }
    }
}
