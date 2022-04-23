using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBot.EnDungeons.FieldGenerators.Chambers {
    public class MokeChamber : Chamber {
        public MokeChamber(Field field) : base(field, new Point(0, 0), new Point(1, 1)) { }
        public MokeChamber(Field field, Point position, Point size) : base(field, position, size) {
            Field = field;
        }
        public override Field Field { get; protected set; }
        public override Field Generate()
            => Field;
    }
}
