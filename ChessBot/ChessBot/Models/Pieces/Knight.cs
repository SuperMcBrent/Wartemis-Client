using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Models.Pieces {
    public class Knight : Piece {
        public Knight(Color color) : base(color) {
            Designator = "n";
        }

        public override void Evaluate() {
            Cell cell;

            Cell forkNorth = Cell.CellToNorth()?.CellToNorth();
            if (forkNorth != null) {
                cell = forkNorth.CellToEast();
                AddToAvailableOrUnavailableTargets(cell);
                AddToProtectingOrEndangeringPieces(cell);
                cell = forkNorth.CellToWest();
                AddToAvailableOrUnavailableTargets(cell);
                AddToProtectingOrEndangeringPieces(cell);
            }
            

            Cell forkSouth = Cell.CellToSouth()?.CellToSouth();
            if (forkSouth != null) {
                cell = forkSouth.CellToEast();
                AddToAvailableOrUnavailableTargets(cell);
                AddToProtectingOrEndangeringPieces(cell);
                cell = forkSouth.CellToWest();
                AddToAvailableOrUnavailableTargets(cell);
                AddToProtectingOrEndangeringPieces(cell);
            }

            Cell forkEast = Cell.CellToEast()?.CellToEast();
            if (forkEast != null) {
                cell = forkEast.CellToNorth();
                AddToAvailableOrUnavailableTargets(cell);
                AddToProtectingOrEndangeringPieces(cell);
                cell = forkEast.CellToSouth();
                AddToAvailableOrUnavailableTargets(cell);
                AddToProtectingOrEndangeringPieces(cell);
            }

            Cell forkWest = Cell.CellToWest()?.CellToWest();
            if (forkWest != null) {
                cell = forkWest.CellToNorth();
                AddToAvailableOrUnavailableTargets(cell);
                AddToProtectingOrEndangeringPieces(cell);
                cell = forkWest.CellToSouth();
                AddToAvailableOrUnavailableTargets(cell);
                AddToProtectingOrEndangeringPieces(cell);
            }

        }
    }
}
