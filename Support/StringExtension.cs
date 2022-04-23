using System;
using System.Collections.Generic;
using System.Text;

namespace EnDronist.Support {
    /// <summary>
    /// Расширение класса String для управления цветом текста
    /// </summary>
    public static class StringExtension {
        public static ColoredString Default(this string str) {
            return new ColoredString(str, ColoredString.DefaultColor);
        }
        public static ColoredString Black(this string str) {
            return new ColoredString(str, ConsoleColor.Black);
        }
        public static ColoredString Blue(this string str) {
            return new ColoredString(str, ConsoleColor.Blue);
        }
        public static ColoredString Cyan(this string str) {
            return new ColoredString(str, ConsoleColor.Cyan);
        }
        public static ColoredString DarkBlue(this string str) {
            return new ColoredString(str, ConsoleColor.DarkBlue);
        }
        public static ColoredString DarkCyan(this string str) {
            return new ColoredString(str, ConsoleColor.DarkCyan);
        }
        public static ColoredString DarkGray(this string str) {
            return new ColoredString(str, ConsoleColor.DarkGray);
        }
        public static ColoredString DarkGreen(this string str) {
            return new ColoredString(str, ConsoleColor.DarkGreen);
        }
        public static ColoredString DarkMagenta(this string str) {
            return new ColoredString(str, ConsoleColor.DarkMagenta);
        }
        public static ColoredString DarkRed(this string str) {
            return new ColoredString(str, ConsoleColor.DarkRed);
        }
        public static ColoredString DarkYellow(this string str) {
            return new ColoredString(str, ConsoleColor.DarkYellow);
        }
        public static ColoredString Gray(this string str) {
            return new ColoredString(str, ConsoleColor.Gray);
        }
        public static ColoredString Green(this string str) {
            return new ColoredString(str, ConsoleColor.Green);
        }
        public static ColoredString Magenta(this string str) {
            return new ColoredString(str, ConsoleColor.Magenta);
        }
        public static ColoredString Red(this string str) {
            return new ColoredString(str, ConsoleColor.Red);
        }
        public static ColoredString White(this string str) {
            return new ColoredString(str, ConsoleColor.White);
        }
        public static ColoredString Yellow(this string str) {
            return new ColoredString(str, ConsoleColor.Yellow);
        }
    }
}
