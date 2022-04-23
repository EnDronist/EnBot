using Discord;
using Discord.WebSocket;
using EnBot.EnDungeons.Drawers;
using EnBot.EnDungeons.Entities.Creatures;
using EnBot.EnDungeons.FieldGenerators;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBot.EnDungeons {
    public class EnDungeonsSession {
        public enum GameStage {
            Menu,
            Lobby,
            Game,
            Pause,
        }
        public Field Field { get; private set; }
        public Dictionary<Player, IMessage> Players { get; private set; } = new Dictionary<Player, IMessage>();
        public Drawer Drawer { get; private set; } = new BaseDrawer();
        public bool IsGameStarted { get; private set; }
        public EnDungeonsSession(SocketMessage message) {
            var firstPlayer = new Player(message.Author);
            Players.Add(firstPlayer, message);
            // Starting game immediately
            Field = new Field(new Point(40, 40));
            // Setting player entity
            Field.Entities.Add(new PlayerEntity(firstPlayer, Field, new Point(10, 10)));
            // Generating map
            new DefaultGenerator().Generate(Field);
        }
        public string Draw() {
            var playerEntity = (PlayerEntity)Field.Entities.Find(entity => entity.GetType() == typeof(PlayerEntity));
            return Drawer.Draw(Field, playerEntity);
        }
    }
}
