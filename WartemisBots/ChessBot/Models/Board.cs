using Chess.Models.Pieces;
using System;
using System.Collections.Generic;

namespace Chess.Models {
    public class Board {
        public List<Cell> Cells { get; set; }
        public PieceFactory PieceFactory { get; private set; }

        // rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1

        public Board(string fen) {
            PieceFactory = new PieceFactory();
            Cells = new List<Cell>();
            for (int i = 0; i < 64; i++) {
                Cells.Add(new Cell(this));
            }
            FenToCells(fen);
        }

        private void FenToCells(string fen) {
            int counter = 0;
            string boardfen = fen.Split(' ')[0];
            string[] ranks = boardfen.Split('/');
            foreach (var rank in ranks) {
                var parts = rank.ToCharArray();
                foreach (var part in parts) {
                    if (char.IsDigit(part)) {
                        counter += int.Parse(part.ToString());
                    } else {
                        Cells[counter].SetPiece(
                            PieceFactory.GetPieceFromDesignator(part.ToString())
                        );
                        counter++;
                    }
                }
            }
        }

        public void Evaluate() {
            foreach (Cell cell in Cells) {
                if (cell.IsEmpty()) continue;

                //if (cell.Piece.GetType() == typeof(Pawn)) continue;
                //if (cell.Piece.GetType() == typeof(Rook)) continue;
                //if (cell.Piece.GetType() == typeof(Knight)) continue;
                //if (cell.Piece.GetType() == typeof(Bishop)) continue;
                if (cell.Piece.GetType() == typeof(King)) continue;
                if (cell.Piece.GetType() == typeof(Queen)) continue;

                cell.Piece.Evaluate();

                //Log.Information("Pawn: {pawn}", cell.Piece);

                //herevalueer
            }
        }

        public Move GetMove() {
            throw new NotImplementedException();
        }
    }
}
