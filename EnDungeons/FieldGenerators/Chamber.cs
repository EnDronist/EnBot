using EnBot.EnDungeons.Cells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBot.EnDungeons.FieldGenerators {
    public abstract class Chamber : IDisposable {
        public enum EmptyZoneType {
            CellType,
            ChamberType,
        }
        public abstract Field Field { get; protected set; }
        public Point Position { get; private set; }
        public Point Size { get; private set; }
        public Chamber(Field field, Point position, Point size) {
            Field = field;
            Position = position;
            Size = size;
        }
        public abstract Field Generate();
        private List<Point> EmptyZoneIncrease(List<Point> emptyZones, Point size, EmptyZoneType emptyZoneType, Type[] cellTypeFilter = null, Type[] chamberTypeFilter = null) {
            // Checking size
            if (size.X < 1 || size.Y < 1) throw new Exception("Point coordinates not positive");
            if (emptyZones == null) emptyZones = new List<Point>();
            // Setting default filters
            if (cellTypeFilter == null) cellTypeFilter = new Type[0];
            if (chamberTypeFilter == null) chamberTypeFilter = new Type[0];
            // Increasing size of zones
            var currentSize = new Point(1, 1);
            bool isWidthChanged;
            int counter = 0;
            while (true) {
                counter++;
                // Checking empty zones count
                if (emptyZones.Count == 0) break;
                // Increasing current size
                if (currentSize.X < size.X) {
                    currentSize.X++;
                    isWidthChanged = true;
                }
                else if (currentSize.Y < size.Y) {
                    currentSize.Y++;
                    isWidthChanged = false;
                }
                else break;
                // Checking new line of zones
                for (var i = 0; i < emptyZones.Count; i++) {
                    var lineSize = isWidthChanged ? currentSize.Y : currentSize.X;
                    for (var j = 0; j < lineSize; j++) {
                        // Checking zone range
                        var currentCellX = isWidthChanged ? emptyZones[i].X + currentSize.X - 1 : emptyZones[i].X + j;
                        var currentCellY = isWidthChanged ? emptyZones[i].Y + j : emptyZones[i].Y + currentSize.Y - 1;
                        //Console.WriteLine($"{i} {j} {emptyZones.Count} {currentCellX} {currentCellY} counter:{counter}");
                        // If cell not passed filter
                        if (currentCellX > Field.Size.X - 1 || currentCellY > Field.Size.Y - 1) {
                            emptyZones.RemoveAt(i);
                            i--;
                            break;
                        }
                        var currentCell = Field.Cells[currentCellY][currentCellX];
                        var isCellPassed = false;
                        if (emptyZoneType == EmptyZoneType.CellType) {
                            // Search only on cells with filter types
                            foreach (var type in cellTypeFilter) {
                                if (currentCell.GetType().IsInstanceOfType(type))
                                    isCellPassed = true;
                            }
                            // Search only on cells with Floor type
                            if (cellTypeFilter.Count() == 0)
                                if (currentCell.GetType().IsInstanceOfType(typeof(Floor)))
                                    isCellPassed = true;
                        }
                        else if (emptyZoneType == EmptyZoneType.ChamberType) {
                            // Search only in chambers
                            foreach (var type in chamberTypeFilter) {
                                if (currentCell.Chamber != null && currentCell.Chamber.GetType().IsInstanceOfType(type))
                                    isCellPassed = true;
                            }
                            // Search zone without chamber
                            if (chamberTypeFilter.Count() == 0)
                                if (currentCell.Chamber == null)
                                    isCellPassed = true;
                        }
                        // If cell not passed filter
                        if (!isCellPassed) {
                            emptyZones.RemoveAt(i);
                            i--;
                            break;
                        }
                    }
                }
            }
            return emptyZones;
        }
        /**
         * <summary>Finding available cell zones (hard algorithm)</summary>
         */
        public List<Point> GetEmptyZonesEasy(Point size, EmptyZoneType emptyZoneType, int triesCount, Type[] cellTypeFilter = null, Type[] chamberTypeFilter = null) {
            // Checking size
            if (size.X < 1 || size.Y < 1) throw new Exception("Point coordinates not positive");
            // Setting default filters
            if (cellTypeFilter == null) cellTypeFilter = new Type[0];
            if (chamberTypeFilter == null) chamberTypeFilter = new Type[0];
            // Trying to find empty zone
            var rand = new Random();
            for (var i = 0; i < triesCount; i++) {
                var startPos = emptyZoneType == EmptyZoneType.CellType
                    ? Position
                    : new Point(0, 0);
                var endPos = emptyZoneType == EmptyZoneType.CellType
                    ? new Point(Position.X + Size.X - 1 - (size.X - 1), Position.Y + Size.Y - 1 - (size.Y - 1))
                    : new Point(Field.Size.X - (size.X - 1), Field.Size.Y - (size.Y - 1));
                var currentPosition = new Point(rand.Next(startPos.X, endPos.X), rand.Next(startPos.Y, endPos.Y));
                // Increasing size of zones
                var emptyZones = new List<Point>() { currentPosition };
                emptyZones = EmptyZoneIncrease(emptyZones, size, emptyZoneType, cellTypeFilter, chamberTypeFilter);
            }
            return null;
        }
        /**
        * <summary>Finding available cell zones (hard algorithm)</summary>
        */
        public List<Point> GetEmptyZonesHard(Point size, EmptyZoneType emptyZoneType, Type[] cellTypeFilter = null, Type[] chamberTypeFilter = null) {
            // Checking size
            if (size.X < 1 || size.Y < 1) throw new Exception("Point coordinates not positive");
            var emptyZones = new List<Point>();
            // Setting default filters
            if (cellTypeFilter == null) cellTypeFilter = new Type[0];
            if (chamberTypeFilter == null) chamberTypeFilter = new Type[0];
            // Filling list
            if (emptyZoneType == EmptyZoneType.CellType) {
                for (var y = Position.Y; y < Position.Y + Size.Y - size.Y + 1; y++) {
                    for (var x = Position.X; x < Position.X + Size.X - size.X + 1; x++) {
                        // Search only on cells with filter types
                        foreach (var type in cellTypeFilter) {
                            if (Field.Cells[y][x].GetType().IsInstanceOfType(type))
                                emptyZones.Add(Field.Cells[y][x].Position);
                        }
                        // Search only on cells with Floor type
                        if (cellTypeFilter.Count() == 0)
                            if (Field.Cells[y][x].GetType().IsInstanceOfType(typeof(Floor)))
                                emptyZones.Add(Field.Cells[y][x].Position);
                    }
                }
            }
            else if (emptyZoneType == EmptyZoneType.ChamberType) {
                for (var y = 0; y < Field.Size.Y - size.Y + 1; y++) {
                    for (var x = 0; x < Field.Size.X - size.X + 1; x++) {
                        // Search only in chambers
                        foreach (var type in chamberTypeFilter) {
                            var chamber = Field.Cells[y][x].Chamber;
                            if (chamber != null && chamber.GetType().IsInstanceOfType(type))
                                emptyZones.Add(Field.Cells[y][x].Position);
                        }
                        // Search zone without chamber
                        if (chamberTypeFilter.Count() == 0) {
                            var chamber = Field.Cells[y][x].Chamber;
                            if (chamber == null)
                                emptyZones.Add(Field.Cells[y][x].Position);
                        }
                    }
                }
            }
            // Increasing size of zones
            emptyZones = EmptyZoneIncrease(emptyZones, size, emptyZoneType, cellTypeFilter, chamberTypeFilter);
            // Finish
            return emptyZones;
        }
        /**
         * <summary>Finding random available cell zone</summary>
         */
        public virtual Point? RandomEmptyCellZoneEasy(Point size, int triesCount, Type[] cellTypeFilter = null) {
            // Finding available zones
            var emptyZones = GetEmptyZonesEasy(size, EmptyZoneType.CellType, triesCount, cellTypeFilter);
            // Get random zone
            var rand = new Random();
            var randPoint = emptyZones[rand.Next(emptyZones.Count)];
            // Finish
            return randPoint;
        }
        /**
         * <summary>Finding random available zone for chamber</summary>
         */
        public Point? RandomEmptyChamberZoneEasy(Point size, int triesCount, Type[] chamberTypeFilter = null) {
            // Finding available zones
            var emptyZones = GetEmptyZonesEasy(size, EmptyZoneType.ChamberType, triesCount, null, chamberTypeFilter);
            if (emptyZones.Count == 0) return null;
            // Get random zone
            var rand = new Random();
            var randPoint = emptyZones[rand.Next(emptyZones.Count)];
            // Finish
            return randPoint;
        }
        /**
         * <summary>Finding random available cell zone (hard algorithm)</summary>
         */
        public virtual Point? RandomEmptyCellZoneHard(Point size, Type[] cellTypeFilter = null) {
            // Finding available zones
            var emptyZones = GetEmptyZonesHard(size, EmptyZoneType.CellType, cellTypeFilter);
            // Get random zone
            var rand = new Random();
            var randPoint = emptyZones[rand.Next(emptyZones.Count)];
            // Finish
            return randPoint;
        }
        /**
         * <summary>Finding random available zone for chamber (hard algorithm)</summary>
         */
        public Point? RandomEmptyChamberZoneHard(Point size, Type[] chamberTypeFilter = null) {
            // Finding available zones
            var emptyZones = GetEmptyZonesHard(size, EmptyZoneType.ChamberType, null, chamberTypeFilter);
            if (emptyZones.Count == 0) return null;
            // Get random zone
            var rand = new Random();
            var randPoint = emptyZones[rand.Next(emptyZones.Count)];
            // Finish
            return randPoint;
        }
        public void Dispose() {
            Field.Chambers.Remove(this);
        }
    }
}