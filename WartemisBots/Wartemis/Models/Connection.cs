using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Wartemis.CustomEventArgs;
using Wartemis.Tools;
using WatsonWebsocket;

namespace Wartemis.Models {
    public class Connection {

        public Uri ConnectionUri { get; private set; }
        public WatsonWsClient Client { get; private set; }
        public bool Connected { get; private set; }
        public bool Registered { get; private set; }
        public MessageBuilder Builder { get; private set; }
        public int IdAssignedByServer { get; private set; }
        public string Name { get; private set; }

        public event EventHandler<StateReceivedEventArgs> OnStateReceived;

        public Connection(string name, string socket) {
            Name = name;
            ConnectionUri = new Uri(socket);
            this.Builder = new MessageBuilder();
        }

        public void StartConnection() {
            if (Client != null) Client.Dispose();
            Client = new WatsonWsClient(this.ConnectionUri);
            Client.ServerConnected += ServerConnected;
            Client.ServerDisconnected += ServerDisconnected;
            Client.MessageReceived += MessageReceived;
            Client.Logger = WSLog;
            Client.Start();
            Thread.Sleep(1000);
        }

        public void StopConnection() {
            if (Client != null) Client.Dispose();
            Connected = false;
            Registered = false;
        }

        public void RaiseOnStateReceivedEventManually(string fakeReceived) {
            Message message = Builder.ParseMessage(fakeReceived);
            Log.Information("[{Name}] FAKE Received {state} message", Name, "state");
            string state = (string)message.GetValue("state");
            OnStateReceivedEvent(state);
        }

        private void MessageReceived(object sender, MessageReceivedEventArgs args) {
            string received = Encoding.UTF8.GetString(args.Data);

            Log.Information("[{Name}] Received: {received}", Name, received.Truncate(75));

            Message message = Builder.ParseMessage(received);
            //Debug.WriteLine(message);
            if (message.GetValue("type").Equals("connected")) {
                Log.Information("[{Name}] Received {connected} message", Name, "connected");
                Connected = true;
            }

            if (message.GetValue("type").Equals("registered")) {
                Log.Information("[{Name}] Received {registered} message", Name, "registered");
                Registered = true;
                IdAssignedByServer = (int)message.GetValue("id");
            }

            if (message.GetValue("type").Equals("state")) {
                Log.Information("[{Name}] Received {state} message", Name, "state");
                string state = (string)message.GetValue("state");
                OnStateReceivedEvent(state);
            }
        }

        private void ServerConnected(object sender, EventArgs args) {
            Log.Information("[{Name}] Connected.", Name);
        }

        private void ServerDisconnected(object sender, EventArgs args) {
            Log.Information("[{Name}] Disconnected", Name);
        }

        private void Transmit(string message) {
            Log.Information("[{Name}] Transmitting: {message}", Name, message);
            if (!this.Client.SendAsync(Encoding.UTF8.GetBytes(message)).Result) {
                Log.Warning("[{Name}] Transmission failed.", Name);
                return;
            }
        }

        public void RegisterConnection(string botName, GameType gametype) {
            Log.Debug("[{Name}] Registering conn", Name);
            Message msg = Builder.PrepareRegisterBotMessage(botName, gametype);
            Log.Debug("[{Name}] Message prepared", Name);
            Task.Run(() => Transmit(msg.GetJSON()));
        }

        public void SendAction(string action) {
            Log.Debug("[{Name}] Sending action", Name);
            Message msg = Builder.PrepareActionMessage(action);
            Log.Debug("[{Name}] Message prepared", Name);
            Task.Run(() => Transmit(msg.GetJSON()));
        }

        private void WSLog(string message) {
            Log.Information("[{Name}] {message}", Name, message);
        }

        protected virtual void OnStateReceivedEvent(string state) {
            var e = new StateReceivedEventArgs(state);
            OnStateReceived?.Invoke(this, e);
        }
    }
}
