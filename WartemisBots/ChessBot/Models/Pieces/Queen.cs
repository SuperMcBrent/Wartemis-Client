using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Models.Pieces {
    public class Queen : Piece {
        public Queen(Color color) : base(color) {
            Designator = "q";
        }

        public override void Evaluate() {
            Cell cell;

            #region Cardinal Movement
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
            #endregion

            #region Diagonal Movement
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
            #endregion
        }
    }
}
