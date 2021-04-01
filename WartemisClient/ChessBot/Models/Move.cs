using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Models {
    public class Move {
        public Cell From { get; set; }
        public Cell To { get; set; }

        public Board BoardAfterMove { get; private set; }

        public Move(Cell from, Cell to, Board boardBeforeMove) {
            From = from;
            To = to;
            BoardAfterMove = new Board(boardBeforeMove, this);
        }

        public override string ToString() {
            return $"{From}[{From.Piece}] => {To}[{To.Piece}]";
        }
    }
}
