using EnBot.CommandSystem.Commands.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBot.CommandSystem.Commands {
    public class TestCluster : CommandCluster {
        public override string Keyword => "test";
        public override string Description => null;
        static List<Command> _subcommands = new List<Command>() {
            new MessageWithButtons(),
        };
        public override IReadOnlyList<ICommand> Subcommands => _subcommands;
    }
}
