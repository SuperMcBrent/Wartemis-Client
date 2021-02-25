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
                    // TODO Only if pawn hasnt moved yet
                    if (Cell.CellToSouth().IsEmpty()) {
                        cell = Cell.CellToSouth().CellToSouth();
                        AddToAvailableOrUnavailableTargets(cell);
                    }

                    // Attack Position Left (Protect/Endanger)
                    cell = Cell.CellToSouthWest();
                    AddToProtectingOrEndangeringPieces(cell);

                    // Attack Position Right (Protect/Endanger)
                    cell = Cell.CellToSouthEast();
                    AddToProtectingOrEndangeringPieces(cell);
                    break;
                case Color.WHITE:
                    // Forward Position (Available/Unavailable)
                    cell = Cell.CellToNorth();
                    AddToAvailableOrUnavailableTargets(cell);

                    // 2x Forward Position (Available/Unavailable)
                    // Cant jump, cell in front needs to be empty
                    // TODO Only if pawn hasnt moved yet
                    if (Cell.CellToNorth().IsEmpty()) {
                        cell = Cell.CellToNorth().CellToNorth();
                        AddToAvailableOrUnavailableTargets(cell);
                    }

                    // Attack Position Left (Protect/Endanger)
                    cell = Cell.CellToNorthWest();
                    AddToProtectingOrEndangeringPieces(cell);

                    // Attack Position Right (Protect/Endanger)
                    cell = Cell.CellToNorthEast();
                    AddToProtectingOrEndangeringPieces(cell);
                    break;
            }
        }
    }
}
