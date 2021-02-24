using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Models.Pieces {
    public class Queen : Piece {
        public Queen(Color color) : base(color) {
            Designator = "q";
        }

        public override void Evaluate() {
            throw new NotImplementedException();
        }
    }
}
