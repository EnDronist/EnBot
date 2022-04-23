using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBot.EnDungeons.Entities {
    public abstract class Entity : IDisposable {
        public Field Field { get; private set; }
        public Point Position { get; private set; }
        public Entity(Field field, Point position) {
            // Checking field
            if (field == null) throw new Exception("Field is null.");
            // Creating
            Field = field;
            Position = position;
            // Additional
            Field.Entities.Add(this);
        }
        public void Dispose() {
            Field.Entities.Remove(this);
        }
    }
}
