using Chess.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Tools {
    public static class Printers {
        public static void PrintNeighboursOfCell(Cell cell) {
            Log.Information("Neighbours of {cell}, {piece}", cell, cell.Piece);
            Log.Information("North:     {cell}, {piece}", cell.CellToNorth(), cell.CellToNorth()?.Piece);
            Log.Information("NorthEast: {cell}, {piece}", cell.CellToNorthEast(), cell.CellToNorthEast()?.Piece);
            Log.Information("East:      {cell}, {piece}", cell.CellToEast(), cell.CellToEast()?.Piece);
            Log.Information("SouthEast: {cell}, {piece}", cell.CellToSouthEast(), cell.CellToSouthEast()?.Piece);
            Log.Information("South:     {cell}, {piece}", cell.CellToSouth(), cell.CellToSouth()?.Piece);
            Log.Information("SouthWest: {cell}, {piece}", cell.CellToSouthWest(), cell.CellToSouthWest()?.Piece);
            Log.Information("West:      {cell}, {piece}", cell.CellToWest(), cell.CellToWest()?.Piece);
            Log.Information("NorthWest: {cell}, {piece}", cell.CellToNorthWest(), cell.CellToNorthWest()?.Piece);
        }

        public static void PrintBoard(Board board, bool withCellNames = false) {
            string boardString = "";
            for (int row = 0; row < 8; row++) {
                for (int col = 0; col < 8; col++) {
                    int index = (row * 8) + col;
                    //Console.Write(Cells[index] + (withCellNames?":"+(Cells[index].Piece?.ToString() ?? " "):"") + " ");
                    boardString += (withCellNames ? (board.Cells[index] + ":") : "") + (board.Cells[index].Piece?.ToString() ?? " ") + " ";
                }
                if (row < 7) boardString += "\n";
            }
            Log.Information("Board: \n{board}", boardString);
        }
    }
}
