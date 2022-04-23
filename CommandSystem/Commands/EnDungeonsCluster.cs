using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnBot.CommandSystem.Commands.EnDungeons;

namespace EnBot.CommandSystem.Commands {
    public class EnDungeonsCluster : CommandCluster {
        public override string Keyword => "endungeons";
        public override string Description => "roguelike rpg game";
        static List<Command> _subcommands = new List<Command>() {
            new Start(),
            new Stop(),
        };
        public override IReadOnlyList<ICommand> Subcommands => _subcommands;
    }
}
