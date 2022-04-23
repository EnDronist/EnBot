using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Discord.WebSocket;
using EnBot.CommandSystem.Commands.Random;

namespace EnBot.CommandSystem.Commands {
    public class RandomCluster : CommandCluster {
        public override string Keyword => "random";
        public override string Description => null;
        static List<Command> _subcommands = new List<Command>() {
            new CandyRandom(),
        };
        public override IReadOnlyList<ICommand> Subcommands => _subcommands;
    }
}