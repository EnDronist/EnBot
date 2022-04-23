using EnBot.EnDungeons.FieldGenerators.Chambers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBot.EnDungeons.FieldGenerators {
    public class DefaultGenerator {
        const int MinChamberSize = 4;
        const int MaxChamberSize = 7;
        private List<Chamber> Chambers = new List<Chamber>();
        public Field Generate(Field field) {
            // Checking field
            if (field == null) return field;
            // Creating chambers
            bool isHardZoneSearch = false;
            while (true) {
                var rand = new Random();
                var randSize = new Point(
                    rand.Next(MinChamberSize, MaxChamberSize),
                    rand.Next(MinChamberSize, MaxChamberSize)
                );
                // Trying to find empty zone
                Point? randPoint = null;
                // Easy search
                if (isHardZoneSearch) {
                    randPoint = new MokeChamber(field).RandomEmptyChamberZoneEasy(randSize, 20);
                    if (randPoint == null) isHardZoneSearch = true;
                }
                // Hard search
                else {
                    randPoint = new MokeChamber(field).RandomEmptyChamberZoneHard(randSize);
                }
                if (randPoint == null) break;
                var chamber = new BaseChamber(field, randPoint.Value, randSize);
                Chambers.Add(chamber);
                chamber.Generate();
                //Console.WriteLine($"GENERATED {{{Chambers.Count}}}");
            }
            // Finish
            return field;
        }
    }
}
