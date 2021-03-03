using Chess.Models.Pieces;
using Chess.Tools;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models {
    public class MoveFactory {
        public List<Move> PotentialMoves { get; private set; }

        public Board Board { get; private set; }
        public MoveComparer MoveComparer { get; private set; }

        public MoveFactory(Board board) {
            Board = board;
            MoveComparer = new MoveComparer();
            PotentialMoves = new List<Move>();
        }

        private void PreparePotentialMoves() {

            int options = 0;

            Stopwatch s = new Stopwatch();
            s.Start();

            // normale zet, weglopen, aanvallen

            var playingpieces = Board.Pieces.Where(p => p.Color.Equals(Board.ColorPlaying));
            foreach (Piece piece in playingpieces) {

                // lege cellen waar ik naartoe kan
                foreach (Cell cell in piece.AvailableEmptyCells) {
                    PotentialMoves.Add(new Move(piece.Cell, cell, Board));
                }

                // cellen met stukken die ik kan aanvallen
                foreach (Cell cell in piece.Endangering.Select(p => p.Cell)) {
                    PotentialMoves.Add(new Move(piece.Cell, cell, Board));
                }

                // cellen met stukken waar momenteel een ally op staat die ik aan het verdedigen ben
                //piece.Protecting.Select(p => p.Cell))
            }

            s.Stop();
            Log.Error("Preparing potential moves took {ms} ms", s.ElapsedMilliseconds);
        }

        public Move GetMove() {

            PreparePotentialMoves();

            // alle PotentialMoves door MoveComparer gooien
            // return MoveComparer.GetBestMove(PotentialMoves);

            //voorlopig voor test: random move uit potentialMoves;

            Move move = PotentialMoves[new Random().Next(0, PotentialMoves.Count)];

            return move;
        }
    }
}
