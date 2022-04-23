using System;
using System.Collections.Generic;
using System.Text;

namespace EnDronist.Support {
    public class ColoredString {
        /// <summary>
        /// Возвращать ли стандартный цвет консоли после каждого вывода цветного текста?
        /// </summary>
        public static bool DoReturnDefaultColor = true;
        /// <summary>
        /// Базовый цвет текста
        /// </summary>
        public static ConsoleColor DefaultColor { get; set; } = ConsoleColor.White;
        /// <summary>
        /// Текст
        /// </summary>
        public string Text { get; }
        /// <summary>
        /// Цвет текста
        /// </summary>
        public ConsoleColor Color { get; }
        public ColoredString(string text) {
            this.Text = text;
            this.Color = DefaultColor;
        }
        public ColoredString(string text, ConsoleColor color) {
            this.Text = text;
            this.Color = color;
        }
        public static ColoredStringCluster operator +(ColoredString lhs, string rhs) {
            return new ColoredStringCluster(lhs, rhs.Default());
        }
        public static ColoredStringCluster operator +(ColoredString lhs, ColoredString rhs) {
            return new ColoredStringCluster(lhs, rhs);
        }
        /// <summary>
        /// Аналог Console.Write для ColoredString
        /// </summary>
        public void Write() {
            Console.ForegroundColor = Color;
            Console.Write(Text);
            if (DoReturnDefaultColor)
                Console.ForegroundColor = DefaultColor;
        }
        /// <summary>
        /// Аналог Console.WriteLine для ColoredString
        /// </summary>
        public void WriteLine() {
            Console.ForegroundColor = Color;
            Console.WriteLine(Text);
            if (DoReturnDefaultColor)
                Console.ForegroundColor = DefaultColor;
        }
    }
}
