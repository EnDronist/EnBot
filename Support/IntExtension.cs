using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBot.Support {
    public static class IntExtension {
        /**
         * <summary>Returns ending of word depending on the case of a russian word formed by a quantitative measure</summary>
         */
        public static string RusCase(this int value, string firstEnding, string secondEnding, string thirdEnding)
            => value % 10 == 1
                ? firstEnding
                : value % 10 < 5
                    ? secondEnding
                    : thirdEnding;
    }
}
