using Chess.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Models.Pieces {
    public class Pawn : Piece {
        public Pawn(Color color) : base(color) {
            Designator = "p";
        }

        public override void Evaluate() {
            Cell cell;
            switch (Color) {
                case Color.BLACK:
                    // Forward Position (Available/Unavailable)
                    cell = Cell.CellToSouth();
                    AddToAvailableOrUnavailableTargets(cell);

                    // 2x Forward Position (Available/Unavailable)
                    // Cant jump, cell in front needs to be empty
                    if (Cell.CellToSouth().IsEmpty() && IsAtStartPosition()) {
                        cell = Cell.CellToSouth().CellToSouth();
                        AddToAvailableOrUnavailableTargets(cell);
                    }

                    // Attack Position Left (Protect/Endanger)
                    cell = Cell.CellToSouthWest();
                    AddToProtectingOrEndangeringPieces(cell);

                    // Attack Position Right (Protect/Endanger)
                    cell = Cell.CellToSouthEast();
                    AddToProtectingOrEndangeringPieces(cell);

                    //TODO En passant
                    break;
                case Color.WHITE:
                    // Forward Position (Available/Unavailable)
                    cell = Cell.CellToNorth();
                    AddToAvailableOrUnavailableTargets(cell);

                    // 2x Forward Position (Available/Unavailable)
                    // Cant jump, cell in front needs to be empty
                    if (Cell.CellToNorth().IsEmpty() && IsAtStartPosition()) {
                        cell = Cell.CellToNorth().CellToNorth();
                        AddToAvailableOrUnavailableTargets(cell);
                    }

                    // Attack Position Left (Protect/Endanger)
                    cell = Cell.CellToNorthWest();
                    AddToProtectingOrEndangeringPieces(cell);

                    // Attack Position Right (Protect/Endanger)
                    cell = Cell.CellToNorthEast();
                    AddToProtectingOrEndangeringPieces(cell);

                    //TODO En Passant
                    break;
            }
        }

        private bool IsAtStartPosition() {
            char rank = Parsers.IndexToText(Cell.Board.Cells.IndexOf(Cell)).ToCharArray()[1];
            switch (Color) {
                case Color.BLACK:
                    if (rank == '7') return true;
                    break;
                case Color.WHITE:
                    if (rank == '2') return true;
                    break;
            }
            return false;
        }
    }
}
