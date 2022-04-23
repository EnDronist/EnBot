using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EnBot.EnBotJsAPI {
    public abstract class ServerFunctionAPI {
        public abstract string URN { get; }
        public class ResponseData {
            public string Get;
            public HttpContent Post;
            public ResponseData(string get, HttpContent post) {
                Get = get;
                Post = post;
            }
        }
        public abstract class Request {
            /**
             * <summary>Validation data in object and returns string when error occured</summary>
             */
            public abstract string Validate();
            /**
             * <summary>Forms internet-link from object</summary>
             */
            public abstract ResponseData ToQuery();
        }
        public abstract class Response {
            /**
             * <summary>Validation data in object and returns string when error occured</summary>
             */
            public abstract string Validate();
        }
        public abstract class Inner {
            /**
             * <summary>Validation data in object and returns string when error occured</summary>
             */
            public abstract string Validate();
        }
    }
}
