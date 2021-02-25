using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Models.Pieces {
    public class Rook : Piece {
        public Rook(Color color) : base(color) {
            Designator = "r";
        }

        public override void Evaluate() {
            Cell cell;

            // go north until end of board or piece is blocking, if blocking piece is enemy add to endangering
            cell = Cell;
            do {
                cell = cell.CellToNorth();
                if (cell is null) break;

                AddToAvailableOrUnavailableTargets(cell);

                if (!cell.IsEmpty()) {
                    AddToProtectingOrEndangeringPieces(cell);
                    break;
                }

            } while (cell != null);

            // go east until end of board or piece is blocking, if blocking piece is enemy add to endangering
            cell = Cell;
            do {
                cell = cell.CellToEast();
                if (cell is null) break;

                AddToAvailableOrUnavailableTargets(cell);

                if (!cell.IsEmpty()) {
                    AddToProtectingOrEndangeringPieces(cell);
                    break;
                }

            } while (cell != null);

            // go south until end of board or piece is blocking, if blocking piece is enemy add to endangering
            cell = Cell;
            do {
                cell = cell.CellToSouth();
                if (cell is null) break;

                AddToAvailableOrUnavailableTargets(cell);

                if (!cell.IsEmpty()) {
                    AddToProtectingOrEndangeringPieces(cell);
                    break;
                }

            } while (cell != null);

            // go west until end of board or piece is blocking, if blocking piece is enemy add to endangering
            cell = Cell;
            do {
                cell = cell.CellToWest();
                if (cell is null) break;

                AddToAvailableOrUnavailableTargets(cell);

                if (!cell.IsEmpty()) {
                    AddToProtectingOrEndangeringPieces(cell);
                    break;
                }

            } while (cell != null);
        }
    }
}
