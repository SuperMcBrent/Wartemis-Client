using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Models.Pieces {
    public class King : Piece {
        public King(Color color) : base(color) {
            Designator = "k";
        }

        public override void Evaluate() {
            Cell cell;

            cell = Cell.CellToNorth();
            AddToAvailableOrUnavailableTargets(cell);
            AddToProtectingOrEndangeringPieces(cell);

            cell = Cell.CellToNorthEast();
            AddToAvailableOrUnavailableTargets(cell);
            AddToProtectingOrEndangeringPieces(cell);

            cell = Cell.CellToEast();
            AddToAvailableOrUnavailableTargets(cell);
            AddToProtectingOrEndangeringPieces(cell);

            cell = Cell.CellToSouthEast();
            AddToAvailableOrUnavailableTargets(cell);
            AddToProtectingOrEndangeringPieces(cell);

            cell = Cell.CellToSouth();
            AddToAvailableOrUnavailableTargets(cell);
            AddToProtectingOrEndangeringPieces(cell);

            cell = Cell.CellToSouthWest();
            AddToAvailableOrUnavailableTargets(cell);
            AddToProtectingOrEndangeringPieces(cell);

            cell = Cell.CellToWest();
            AddToAvailableOrUnavailableTargets(cell);
            AddToProtectingOrEndangeringPieces(cell);

            cell = Cell.CellToNorthWest();
            AddToAvailableOrUnavailableTargets(cell);
            AddToProtectingOrEndangeringPieces(cell);

            // TODO Castling
        }
    }
}
