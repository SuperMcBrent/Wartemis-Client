using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Models.Pieces {
    public class Knight : Piece {
        public Knight(Color color) : base(color) {
            Designator = "n";
        }
    }
}
