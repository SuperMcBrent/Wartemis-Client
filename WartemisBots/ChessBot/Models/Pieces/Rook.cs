using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Models.Pieces {
    public class Rook : Piece {
        public Rook(Color color) : base(color) {
            Designator = "r";
        }

        public override void Evaluate() {
            throw new NotImplementedException();
        }
    }
}
