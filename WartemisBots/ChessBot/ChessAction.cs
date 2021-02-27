using Chess.Models;
using Chess.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess {
    public class ChessAction : Wartemis.Models.Action {

        public Move Move { get; private set; }

        public ChessAction(Move move) {
            Move = move;
            Raw = $"{{\"from\":{Parsers.TextToIndex(move.From.ToString())}, \"to\":{Parsers.TextToIndex(move.To.ToString())}}}";
        }
    }
}
