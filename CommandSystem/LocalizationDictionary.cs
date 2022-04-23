using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EnBot.CommandSystem {
    public class LocalizationDictionary : ReadOnlyDictionary<string, IReadOnlyDictionary<CommandParser.Language, Func<object[], string>>> {
        public class LocalizationDictionaryBase : Dictionary<string, IReadOnlyDictionary<CommandParser.Language, Func<object[], string>>> { }
        public class LocalizationDictionaryEntry : Dictionary<CommandParser.Language, Func<object[], string>> { }
        public LocalizationDictionary(IDictionary<string, IReadOnlyDictionary<CommandParser.Language, Func<object[], string>>> dictionary) : base(dictionary) {}
        public string Get(string key, object[] args = null) => this[key][CommandParser.CurrentLanguage](args);
    }
}