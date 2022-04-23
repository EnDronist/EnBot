using System;
using System.Collections.Generic;
using System.Text;

namespace EnDronist.Support {
    public class ColoredStringCluster {
        public List<ColoredString> ColoredStrings { get; } = new List<ColoredString>();
        public ColoredStringCluster(params ColoredString[] coloredStrings) {
            this.ColoredStrings.AddRange(coloredStrings);
        }
        public static ColoredStringCluster operator +(ColoredStringCluster lhs, string rhs) {
            lhs.ColoredStrings.Add(rhs.Default());
            return lhs;
        }
        public static ColoredStringCluster operator +(ColoredStringCluster lhs, ColoredString rhs) {
            lhs.ColoredStrings.Add(rhs);
            return lhs;
        }
        public static ColoredStringCluster operator +(ColoredStringCluster lhs, ColoredStringCluster rhs) {
            lhs.ColoredStrings.AddRange(rhs.ColoredStrings);
            return lhs;
        }
        /// <summary>
        /// Аналог Console.Write для ColoredStringCluster
        /// </summary>
        public void Write() {
            var doReturnDefaultColor = ColoredString.DoReturnDefaultColor;
            ColoredString.DoReturnDefaultColor = false;
            foreach (var coloredString in ColoredStrings)
                coloredString.Write();
            ColoredString.DoReturnDefaultColor = doReturnDefaultColor;
            if (ColoredString.DoReturnDefaultColor)
                Console.ForegroundColor = ColoredString.DefaultColor;
        }
        /// <summary>
        /// Аналог Console.WriteLine для ColoredStringCluster
        /// </summary>
        public void WriteLine() {
            var doReturnDefaultColor = ColoredString.DoReturnDefaultColor;
            ColoredString.DoReturnDefaultColor = false;
            foreach (var coloredString in ColoredStrings)
                coloredString.Write();
            Console.WriteLine();
            ColoredString.DoReturnDefaultColor = doReturnDefaultColor;
            if (ColoredString.DoReturnDefaultColor)
                Console.ForegroundColor = ColoredString.DefaultColor;
        }
    }
}
