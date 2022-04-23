using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBot.EnBotJsAPI.ServerFunctions {
    public class Authorization : ServerFunctionAPI {
        public override string URN => "create-buttons";
        public new class Request : ServerFunctionAPI.Request {
            [JsonProperty("token")]
            public string Token { get; private set; }
            public override string Validate() {
                // Token
                if (Token == null)
                    return "Token is null";
                return null;
            }
            public Request(string token) {
                Token = token;
            }
            public override ResponseData ToQuery() {
                var error = Validate();
                if (error != null) throw new Exception(error);
                return ServerAPI.QueryFormat($"{new Authorization().URN}?token={Token}", null);
            }
        }
    }
}
