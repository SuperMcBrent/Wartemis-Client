using Chess.Models.Pieces;
using System;
using System.Collections.Generic;
using System.Text;
using Chess.Tools;

namespace Chess.Models {
    public class Cell {
        public Piece Piece { get; set; }
        public Board Board { get; set; }

        public Cell(Board board) {
            Board = board;
        }

        public bool IsEmpty() {
            return Piece is null;
        }

        public void SetPiece(Piece piece) {
            if (piece is null) return;
            Piece = piece;
        }

        public override string ToString() {
            int index = Board.Cells.IndexOf(this);
            return $"{Parsers.IndexToText(index)}";
        }
    }
}
