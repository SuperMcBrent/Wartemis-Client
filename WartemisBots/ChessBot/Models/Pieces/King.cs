using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Models.Pieces {
    public class King : Piece {
        public King(Color color) : base(color) {
            Designator = "k";
        }
    }
}
