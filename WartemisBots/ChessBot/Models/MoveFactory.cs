using Chess.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Models {
    public class MoveFactory {

        public Board Board { get; private set; }

        public MoveFactory(Board board) {
            Board = board;
        }

        public Move GetTestMove() {
            Cell from = null;
            Cell to = null;
            string rank = Board.ColorPlaying == Color.WHITE ? "2" : "7";
            foreach (char file in Parsers.Files) {
                from = Board.Cells[Parsers.TextToIndex("" + file + rank)];
                if (from.IsEmpty()) continue;
                switch (Board.ColorPlaying) {
                    case Color.BLACK:
                        to = from.CellToSouth().CellToSouth();
                        break;
                    case Color.WHITE:
                        to = from.CellToNorth().CellToNorth();
                        break;
                }
            }

            return new Move(from, to);
        }
    }
}
