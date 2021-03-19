using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Models.Pieces {
    public class Bishop : Piece {
        public Bishop(Color color) : base(color) {
            Designator = "b";
        }

        public override void Evaluate() {
            Cell cell;

            // go northeast until end of board or piece is blocking, if blocking piece is enemy add to endangering
            cell = Cell;
            do {
                cell = cell.CellToNorthEast();
                if (cell is null) break;

                AddToAvailableOrUnavailableTargets(cell);

                if (!cell.IsEmpty()) {
                    AddToProtectingOrEndangeringPieces(cell);
                    break;
                }

            } while (cell != null);

            // go southeast until end of board or piece is blocking, if blocking piece is enemy add to endangering
            cell = Cell;
            do {
                cell = cell.CellToSouthEast();
                if (cell is null) break;

                AddToAvailableOrUnavailableTargets(cell);

                if (!cell.IsEmpty()) {
                    AddToProtectingOrEndangeringPieces(cell);
                    break;
                }

            } while (cell != null);

            // go southwest until end of board or piece is blocking, if blocking piece is enemy add to endangering
            cell = Cell;
            do {
                cell = cell.CellToSouthWest();
                if (cell is null) break;

                AddToAvailableOrUnavailableTargets(cell);

                if (!cell.IsEmpty()) {
                    AddToProtectingOrEndangeringPieces(cell);
                    break;
                }

            } while (cell != null);

            // go northwest until end of board or piece is blocking, if blocking piece is enemy add to endangering
            cell = Cell;
            do {
                cell = cell.CellToNorthWest();
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
