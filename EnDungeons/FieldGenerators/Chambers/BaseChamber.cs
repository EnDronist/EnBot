using EnBot.EnDungeons.Cells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBot.EnDungeons.FieldGenerators.Chambers {
    public class BaseChamber : Chamber {
        public BaseChamber(Field field, Point position, Point size) : base(field, position, size) { }
        public override Field Field { get; protected set; }
        public override Field Generate() {
            // Checking field size
            if (Field.Size.X < Position.X + Size.X - 1 || Field.Size.Y < Position.Y + Size.Y - 1)
                return null;
            // Creating chamber
            for (var y = Position.Y; y < Position.Y + Size.Y; y++) {
                for (var x = Position.X; x < Position.X + Size.X; x++) {
                    if (x == Position.X || x == Position.X + Size.X - 1 || y == Position.Y || y == Position.Y + Size.Y - 1) {
                        Field.Cells[y][x] = new Wall(Field.Cells[y][x].Position);
                    }
                    Field.Cells[y][x].Chamber = this;
                }
            }
            // Finish
            return Field;
        }
    }
}