using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EnBot.EnBotJsAPI {
    public class CreateButtons : ServerFunctionAPI {
        public override string URN => "create-buttons";
        public new class Response : ServerFunctionAPI.Response {
            [JsonProperty("channel")]
            public string Channel { get; private set; }
            [JsonProperty("content")]
            public string Content { get; private set; }
            [JsonProperty("buttons")]
            public List<Button> Buttons { get; private set; }
            public class Button : Inner {
                [JsonProperty("text")]
                public string Text { get; private set; }
                [JsonProperty("color")]
                public string Color { get; private set; }
                public static class Colors {
                    public static readonly string Blurple = "blurple";
                    public static readonly string Grey = "grey";
                    public static readonly string Green = "green";
                    public static readonly string Red = "red";
                }
                public Button(string text, string color) {
                    Text = text;
                    Color = color;
                }
                public override string Validate() {
                    // Text
                    if (!Regex.IsMatch(Text, @"^.{1,64}$"))
                        return "Buttons.Text name is not valid";
                    // Color
                    if (!Regex.IsMatch(Color, @"^(blurple|grey|green|red)$"))
                        return "Buttons.Color name is not valid";
                    return null;
                }
            }
            public Response(string channel, string content, List<Button> buttons) {
                Channel = channel;
                Content = content;
                Buttons = buttons;
            }
            public override string Validate() {
                string error = null;
                // Channel
                // Buttons
                foreach (var button in Buttons)
                    if ((error = button.Validate()) != null)
                        return error;
                return error;
            }
        }
    }
}