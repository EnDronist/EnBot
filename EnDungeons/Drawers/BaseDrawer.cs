using Discord;
using EnBot.EnDungeons.Cells;
using EnBot.EnDungeons.Entities.Creatures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EnBot.EnDungeons.Drawers {
    public class BaseDrawer : Drawer {
        private IReadOnlyDictionary<Type, string> textures = new Dictionary<Type, string>() {
            { typeof(Floor), " " },
            { typeof(Wall), "#" },
        };
        public override IReadOnlyDictionary<Type, string> Textures => textures;
        public override Point DrawingWindow => new Point(40, 40);
        public override string Draw(Field field, PlayerEntity playerEntity) {
            var result = "```";
            result += $"┍{new string('━', DrawingWindow.X)}┑\n";
            var startX = playerEntity.Position.X - DrawingWindow.X / 2;
            startX = startX < 0
                ? 0
                : startX + DrawingWindow.X - 1 > field.Size.X - 1
                    ? field.Size.X - DrawingWindow.X
                    : startX;
            var startY = playerEntity.Position.Y - DrawingWindow.Y / 2;
            startY = startY < 0
                ? 0
                : startY + DrawingWindow.Y - 1 > field.Size.Y - 1
                    ? field.Size.Y - DrawingWindow.Y
                    : startX;
            //foreach (var cellsLine in field.Cells) {
            //    foreach (var cell in cellsLine)
            //        result += Textures[cell.GetType()];
            //    result += '\n';
            //}
            for (var y = startY; y < startY + DrawingWindow.Y; y++) {
                result += '│';
                for (var x = startX; x < startX + DrawingWindow.X; x++) {
                    result += Textures[field.Cells[y][x].GetType()];
                }
                result += '│';
                result += '\n';
            }
            result += $"┕{new string('━', DrawingWindow.X)}┙\n";
            result += "```";
            return result;
        }
    }
}
