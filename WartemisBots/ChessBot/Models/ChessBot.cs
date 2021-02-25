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
        public ChessState State { get; private set; }
        // list of states?

        public ChessBot(string name) : base(name, GameType.Chess) {
            base.Connection.OnStateReceived += ReceivedState;
        }

        private void ReceivedState(object sender, StateReceivedEventArgs e) {
            State = new ChessState(e.State);
            Board = new Board(fen: State.Fen);

            // TODO task / multithread

            Board.Evaluate();

            //do something

            //SendMove(Board.GetMove());
        }

        private void SendMove(Move move) {
            ChessAction action = new ChessAction(move);
            base.Connection.SendAction(action.Raw);
        }
    }
}
