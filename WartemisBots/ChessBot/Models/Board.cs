﻿using Chess.Models.Pieces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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

                if (cell.Piece.GetType() != typeof(Pawn)) continue;

                cell.Piece.Evaluate();

                Log.Information("Pawn: {pawn}", cell.Piece);

                // waar kan ik naartoe
                // waar kan ik niet naartoe
                // wie bedreigt mij
                // wie beschermt mij
                // wie kan ik aanvallen

                //herevalueer
            }
        }

        public void PrintBoard(bool withCellNames = false) {
            string board = "";
            for (int row = 0; row < 8; row++) {
                for (int col = 0; col < 8; col++) {
                    int index = (row * 8) + col;
                    //Console.Write(Cells[index] + (withCellNames?":"+(Cells[index].Piece?.ToString() ?? " "):"") + " ");
                    board += (withCellNames ? (Cells[index] + ":") : "") + (Cells[index].Piece?.ToString() ?? " ") + " ";
                }
                if (row < 7) board += "\n";
            }
            Log.Information("Board: \n{board}", board);
        }

        public Move GetMove() {
            throw new NotImplementedException();
        }
    }
}
