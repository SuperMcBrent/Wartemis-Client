using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Models {
    public class Move {
        public Cell From { get; set; }
        public Cell To { get; set; }

        public Move(Cell from, Cell to) {
            From = from;
            To = to;
        }

        public override string ToString() {
            return $"{From} => {To}";
        }
    }
}
