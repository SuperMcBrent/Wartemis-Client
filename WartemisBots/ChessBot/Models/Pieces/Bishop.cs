using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Models.Pieces {
    public class Bishop : Piece {
        public Bishop(Color color) : base(color) {
            Designator = "b";
        }
    }
}
