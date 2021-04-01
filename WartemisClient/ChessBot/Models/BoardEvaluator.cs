using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chess.Models {
    public class BoardEvaluator {

        public List<Task> TaskList { get; private set; }

        public BoardEvaluator() {
            TaskList = new List<Task>();
        }

        public void Evaluate(Board board) {
            var task = new Task(() => StartEvaluation(board));
            task.Start();
            TaskList.Add(task);
        }

        private void StartEvaluation(Board board) {
            foreach (Cell cell in board.Cells.Where(c => !c.IsEmpty())) {
                //if (cell.IsEmpty()) continue;

                //if (cell.Piece.GetType() == typeof(Pawn)) continue;
                //if (cell.Piece.GetType() == typeof(Rook)) continue;
                //if (cell.Piece.GetType() == typeof(Knight)) continue;
                //if (cell.Piece.GetType() == typeof(Bishop)) continue;
                //if (cell.Piece.GetType() == typeof(King)) continue;
                //if (cell.Piece.GetType() == typeof(Queen)) continue;

                cell.Piece.Evaluate();
            }
            board.EndangeredPieces.AddRange(
                board.Cells
                    .Where(c => !c.IsEmpty())
                    .Where(c => c.Piece.IsEndangered())
                    .Select(c => c.Piece)
            );
            board.EndangeringPieces.AddRange(
                board.Cells
                    .Where(c => !c.IsEmpty())
                    .Where(c => c.Piece.IsEndangering())
                    .Select(c => c.Piece)
            );
            board.NotEndangeredPieces.AddRange(
                board.Cells
                    .Where(c => !c.IsEmpty())
                    .Where(c => !c.Piece.IsEndangered())
                    .Select(c => c.Piece)
            );
            board.ProtectedPieces.AddRange(
                board.Cells
                    .Where(c => !c.IsEmpty())
                    .Where(c => c.Piece.IsProtected())
                    .Select(c => c.Piece)
            );
            board.ProtectingPieces.AddRange(
                board.Cells
                    .Where(c => !c.IsEmpty())
                    .Where(c => c.Piece.IsProtecting())
                    .Select(c => c.Piece)
            );
            board.NotProtectedPieces.AddRange(
                board.Cells
                    .Where(c => !c.IsEmpty())
                    .Where(c => !c.Piece.IsProtected())
                    .Select(c => c.Piece)
            );
        }
    }
}
