using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBot {
    public class JsHelperMessage {
        public static int Port { get; } = 3002;
        [JsonProperty("channel")]
        public string kekw;
    }
}
