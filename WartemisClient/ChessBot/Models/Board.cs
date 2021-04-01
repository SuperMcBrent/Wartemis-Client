using Chess.Models.Pieces;
using Chess.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chess.Models {
    public class Board {
        public List<Cell> Cells { get; set; }
        public PieceFactory PieceFactory { get; private set; }
        public MoveFactory MoveFactory { get; private set; }
        public BoardEvaluator BoardEvaluator { get; private set; }

        public Color ColorPlaying { get; private set; }
        public List<Piece> Pieces => Cells.Where(c => !c.IsEmpty()).Select(c => c.Piece).ToList();

        public List<Piece> EndangeredPieces { get; private set; }
        public List<Piece> EndangeringPieces { get; private set; }
        public List<Piece> NotEndangeredPieces { get; private set; }
        public List<Piece> ProtectedPieces { get; private set; }
        public List<Piece> ProtectingPieces { get; private set; }
        public List<Piece> NotProtectedPieces { get; private set; }

        private Board() {
            Init();
        }

        public Board(string fen) : this() {
            DeconstructFen(fen);
            BoardEvaluator.Evaluate(this);
        }

        public Board(Board board, Move move) : this() {
            for (int i = 0; i < board.Cells.Count; i++) {
                Cell cell = board.Cells[i];
                if (cell.IsEmpty()) continue;
                Cells[i].SetPiece(
                    PieceFactory.GetPieceFromDesignator(cell.Piece.ToString())
                ); 
            }
            ColorPlaying = board.ColorPlaying == Color.WHITE ? Color.BLACK : Color.WHITE;
            BoardEvaluator = board.BoardEvaluator;
            MakeMove(move);
            BoardEvaluator.Evaluate(this);
        }

        private void Init() {
            EndangeredPieces = new List<Piece>();
            EndangeringPieces = new List<Piece>();
            NotEndangeredPieces = new List<Piece>();
            ProtectedPieces = new List<Piece>();
            ProtectingPieces = new List<Piece>();
            NotProtectedPieces = new List<Piece>();
            PieceFactory = new PieceFactory();
            MoveFactory = new MoveFactory(this);
            BoardEvaluator = new BoardEvaluator();
            Cells = new List<Cell>();
            for (int i = 0; i < 64; i++) {
                Cells.Add(new Cell(this));
            }
        }

        private void DeconstructFen(string fen) {

            int counter = 0;
            string boardfen = fen.Split(' ')[0];
            string[] ranks = boardfen.Split('/');
            foreach (var rank in ranks) {
                var parts = rank.ToCharArray();
                foreach (var part in parts) {
                    if (char.IsDigit(part)) {
                        counter += int.Parse(part.ToString());
                    } else {
                        Cells[counter].SetPiece(
                            PieceFactory.GetPieceFromDesignator(part.ToString())
                        );
                        counter++;
                    }
                }
            }

            string colorPlaying = fen.Split(' ')[1];
            ColorPlaying = colorPlaying == "w" ? Color.WHITE : Color.BLACK;
        }

        public void Evaluate() {
            foreach (Cell cell in Cells) {
                if (cell.IsEmpty()) continue;

                //if (cell.Piece.GetType() == typeof(Pawn)) continue;
                //if (cell.Piece.GetType() == typeof(Rook)) continue;
                //if (cell.Piece.GetType() == typeof(Knight)) continue;
                //if (cell.Piece.GetType() == typeof(Bishop)) continue;
                //if (cell.Piece.GetType() == typeof(King)) continue;
                //if (cell.Piece.GetType() == typeof(Queen)) continue;

                cell.Piece.Evaluate();
            }
            EndangeredPieces.AddRange(
                Cells
                    .Where(c => !c.IsEmpty())
                    .Where(c => c.Piece.IsEndangered())
                    .Select(c => c.Piece)
            );
            EndangeringPieces.AddRange(
                Cells
                    .Where(c => !c.IsEmpty())
                    .Where(c => c.Piece.IsEndangering())
                    .Select(c => c.Piece)
            );
            NotEndangeredPieces.AddRange(
                Cells
                    .Where(c => !c.IsEmpty())
                    .Where(c => !c.Piece.IsEndangered())
                    .Select(c => c.Piece)
            );
            ProtectedPieces.AddRange(
                Cells
                    .Where(c => !c.IsEmpty())
                    .Where(c => c.Piece.IsProtected())
                    .Select(c => c.Piece)
            );
            ProtectingPieces.AddRange(
                Cells
                    .Where(c => !c.IsEmpty())
                    .Where(c => c.Piece.IsProtecting())
                    .Select(c => c.Piece)
            );
            NotProtectedPieces.AddRange(
                Cells
                    .Where(c => !c.IsEmpty())
                    .Where(c => !c.Piece.IsProtected())
                    .Select(c => c.Piece)
            );
        }

        public Move GetPotentialMove() {
            return MoveFactory.GetMove();
        }

        private void MakeMove(Move move) {
            Cell fromCell = Cells[Parsers.TextToIndex(move.From.ToString())];
            Cell toCell = Cells[Parsers.TextToIndex(move.To.ToString())];
            Piece movingPiece = fromCell.Piece;

            // set cell piece and piece cell
            toCell.SetPiece(movingPiece);

            // clear cell piece
            fromCell.ClearPiece();
        }
    }
}
