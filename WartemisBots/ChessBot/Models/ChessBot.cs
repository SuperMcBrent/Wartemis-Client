using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Wartemis.CustomEventArgs;
using Wartemis.Interfaces;
using Wartemis.Models;

namespace Chess.Models {
    public class ChessBot : Bot {

        public Board Board { get; private set; }

        public ChessBot(string name) : base(name, GameType.Chess) {
            base.Connection.OnStateReceived += ReceivedState;
        }

        private void ReceivedState(object sender, StateReceivedEventArgs e) {
            dynamic fen = JObject.Parse(e.State);
            Board = new Board(fen: (string)fen.fen);

            //do something

            //SendMove(Board.GetMove());
        }

        private void SendMove(Move move) {
            string action = $"{{\"from\":{move.From}, \"to\":{move.To}}}";
            base.Connection.SendAction(action);
        }
    }
}
