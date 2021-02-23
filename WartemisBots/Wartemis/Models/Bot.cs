using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using Wartemis.CustomEventArgs;
using Wartemis.Tools;

namespace Wartemis.Models {
    public abstract class Bot {
        public Connection Connection { get; private set; }
        public GameType GameType { get; private set; }
        public string Name { get; private set; }

        public Bot(string name, GameType gameType) {
            Name = name;
            GameType = gameType;
            Connection = new Connection(name, "ws://api.wartemis.com/socket");
            Connection.OnStateReceived += ReceivedState;
        }

        public void Start() {
            if (Connection.Connected) return;
            Connection.StartConnection();
            Connection.RegisterConnection(Name, GameType);
        }

        private void ReceivedState(object sender, StateReceivedEventArgs e) {
            Log.Information("[{Name}] State: {state}", Name, e.State.Replace(Environment.NewLine, "").Replace(" ", "").Truncate(75));
        }

    }
}
