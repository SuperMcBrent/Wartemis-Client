using Chess.Models;
using Chess.Models.Pieces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Tools {
    public static class Printers {
        public static void PrintNeighboursOfCell(Cell cell) {
            if (cell is null) return;
            Log.Information("---------- Cell - Piece");
            Log.Information("[Origin]:  {cell,-6} {piece,-6}", cell, cell.Piece);
            Log.Information("North:     {cell,-6} {piece,-6}", cell.CellToNorth(), cell.CellToNorth()?.Piece);
            Log.Information("NorthEast: {cell,-6} {piece,-6}", cell.CellToNorthEast(), cell.CellToNorthEast()?.Piece);
            Log.Information("East:      {cell,-6} {piece,-6}", cell.CellToEast(), cell.CellToEast()?.Piece);
            Log.Information("SouthEast: {cell,-6} {piece,-6}", cell.CellToSouthEast(), cell.CellToSouthEast()?.Piece);
            Log.Information("South:     {cell,-6} {piece,-6}", cell.CellToSouth(), cell.CellToSouth()?.Piece);
            Log.Information("SouthWest: {cell,-6} {piece,-6}", cell.CellToSouthWest(), cell.CellToSouthWest()?.Piece);
            Log.Information("West:      {cell,-6} {piece,-6}", cell.CellToWest(), cell.CellToWest()?.Piece);
            Log.Information("NorthWest: {cell,-6} {piece,-6}", cell.CellToNorthWest(), cell.CellToNorthWest()?.Piece);
        }

        public static void PrintBoard(Board board, bool withCellNames = false) {
            if (board is null) return;
            string boardString = "";
            if (withCellNames) boardString += "  " + String.Join(" ", Parsers.Files) + "\n";
            for (int row = 0; row < 8; row++) {
                if (withCellNames) boardString += "" + Parsers.Ranks[row] + " ";
                for (int col = 0; col < 8; col++) {
                    int index = (row * 8) + col;
                    boardString += (board.Cells[index].Piece?.ToString() ?? " ") + " ";
                }
                if (withCellNames) boardString += "" + Parsers.Ranks[row] + " ";
                if (row < 7) boardString += "\n";
            }
            if (withCellNames) boardString += "\n  " + String.Join(" ", Parsers.Files);
            Log.Information("Board: \n{board}", boardString);
        }

        public static void PrintPieceEvaluation(Piece piece) {
            if (piece is null) return;

            string moveToTargets = String.Join<Cell>(", ", piece.AvailableEmptyCells);
            Log.Information("Piece {piece}[{cell}] can move to {cells}.", piece, piece.Cell, moveToTargets == "" ? "nowhere" : moveToTargets);

            string protectingPieces = String.Join<string>(", ", piece.Protecting.Select(p => $"{p}[{p.Cell}]"));
            Log.Information("Piece {piece}[{cell}] is protecting {cells}.", piece, piece.Cell, protectingPieces == "" ? "noone" : protectingPieces);

            string protectedByPieces = String.Join<string>(", ", piece.ProtectedBy.Select(p => $"{p}[{p.Cell}]"));
            Log.Information("Piece {piece}[{cell}] is protected by {cells}.", piece, piece.Cell, protectedByPieces == "" ? "noone" : protectedByPieces);

            string endangeringPieces = String.Join<string>(", ", piece.Endangering.Select(p => $"{p}[{p.Cell}]"));
            Log.Information("Piece {piece}[{cell}] can attack {cells}.", piece, piece.Cell, endangeringPieces == "" ? "noone" : endangeringPieces);

            string endangeredByPieces = String.Join<string>(", ", piece.EndangeredBy.Select(p => $"{p}[{p.Cell}]"));
            Log.Information("Piece {piece}[{cell}] can be attacked by {cells}.", piece, piece.Cell, endangeredByPieces == "" ? "noone" : endangeredByPieces);
        }

        public static void PrintBoardEvaluation(Board board, bool extensive = false) {
            if (board is null) return;
            bool printedSomething = false;

            foreach (var color in Enum.GetValues(typeof(Color))) {

                var endangeredPieces = board.EndangeredPieces.Where(p => p.Color.Equals(color)).ToList();
                if (endangeredPieces.Count > 0) {
                    printedSomething = true;
                    Log.Information("Board has {count} {color} piece(s) that are in danger.", endangeredPieces.Count, color.ToString());
                    foreach (Piece piece in endangeredPieces) {
                        foreach (Piece threat in piece.EndangeredBy) {
                            if (extensive) Log.Information("Piece {0}[{1}] is threatened by {2}[{3}]", piece, piece.Cell, threat, threat.Cell);
                        }
                    }
                }

                var notEndangeredPieces = board.NotEndangeredPieces.Where(p => p.Color.Equals(color)).ToList();
                if (notEndangeredPieces.Count > 0) {
                    printedSomething = true;
                    Log.Information("Board has {count} {color} piece(s) that are in the clear.", notEndangeredPieces.Count, color.ToString());
                }

                var endangeringPieces = board.EndangeringPieces.Where(p => p.Color.Equals(color)).ToList();
                if (endangeredPieces.Count > 0) {
                    printedSomething = true;
                    Log.Information("Board has {count} {color} piece(s) that are threatening another piece.", endangeringPieces.Count, color.ToString());
                    foreach (Piece piece in endangeringPieces) {
                        foreach (Piece target in piece.Endangering) {
                            if (extensive) Log.Information("Piece {0}[{1}] is threatening {2}[{3}]", piece, piece.Cell, target, target.Cell);
                        }
                    }
                }

                var protectedPieces = board.ProtectedPieces.Where(p => p.Color.Equals(color)).ToList();
                if (protectedPieces.Count > 0) {
                    printedSomething = true;
                    Log.Information("Board has {count} {color} piece(s) that are being protected.", protectedPieces.Count, color.ToString());
                    foreach (Piece piece in protectedPieces) {
                        foreach (Piece protector in piece.ProtectedBy) {
                            if (extensive) Log.Information("Piece {0}[{1}] is protected by {2}[{3}]", piece, piece.Cell, protector, protector.Cell);
                        }
                    }
                }

                var notProtectedPieces = board.NotProtectedPieces.Where(p => p.Color.Equals(color)).ToList();
                if (notProtectedPieces.Count > 0) {
                    printedSomething = true;
                    Log.Information("Board has {count} {color} piece(s) that are not protected.", notProtectedPieces.Count, color.ToString());
                }

                var protectingPieces = board.ProtectingPieces.Where(p => p.Color.Equals(color)).ToList();
                if (protectingPieces.Count > 0) {
                    printedSomething = true;
                    Log.Information("Board has {count} {color} piece(s) that are protecting another piece.", protectingPieces.Count, color.ToString());
                    foreach (Piece piece in protectingPieces) {
                        foreach (Piece target in piece.Protecting) {
                            if (extensive) Log.Information("Piece {0}[{1}] is protecting {2}[{3}]", piece, piece.Cell, target, target.Cell);
                        }
                    }
                }
            }

            if (!printedSomething) Log.Information("Nothing to report.");
        }
    }
}
