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
            if (piece is null) return; // no piece passed
            if (Piece != null) return; // cell is occupied
            piece.Position = this;
            Piece = piece;
        }

        public override string ToString() {
            int index = Board.Cells.IndexOf(this);
            return $"{Parsers.IndexToText(index)}";
        }

        #region Neighbouring Cells
        public Cell CellToNorth() {
            Cell cellToNorth = null;
            int index = Board.Cells.IndexOf(this);
            try {
                cellToNorth = Board.Cells[index - 8];
            } catch (Exception) {

            }
            return cellToNorth;
        }

        public Cell CellToSouth() {
            Cell cellToSouth = null;
            int index = Board.Cells.IndexOf(this);
            try {
                cellToSouth = Board.Cells[index + 8];
            } catch (Exception) {

            }
            return cellToSouth;
        }

        public Cell CellToEast() {
            Cell cellToEeast = null;
            int index = Board.Cells.IndexOf(this);
            try {
                if (index % 7 == 0) throw new Exception();
                cellToEeast = Board.Cells[index + 1];
            } catch (Exception) {

            }
            return cellToEeast;
        }

        public Cell CellToWest() {
            Cell cellToWest = null;
            int index = Board.Cells.IndexOf(this);
            try {
                if (index % 8 == 0) throw new Exception();
                cellToWest = Board.Cells[index - 1];
            } catch (Exception) {

            }
            return cellToWest;
        }
        #endregion
    }
}
