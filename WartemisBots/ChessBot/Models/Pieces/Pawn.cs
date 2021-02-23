﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Models.Pieces {
    public class Pawn : Piece {
        public Pawn(Color color) : base(color) {
            Designator = "p";
        }
    }
}
