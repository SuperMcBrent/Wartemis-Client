using Chess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Interfaces {
    public interface IPiece {
        public void EvaluateBoard(Board board);
    }
}
