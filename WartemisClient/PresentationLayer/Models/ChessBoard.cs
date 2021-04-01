using PresentationLayer.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using PresentationLayer.Tools;

namespace PresentationLayer.Models {
    [Serializable]
    public class ChessBoard : Notifier {

        private string _fen;
        private BitmapImage _chessBoard;

        public ChessBoard(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1") {
            Fen = fen;
        }

        public string Fen {
            get => _fen;
            set {
                if (_fen != value) {
                    _fen = value;
                    RaisePropertyChanged(() => Fen);

                    Board = ChessImages.BoardFromFen(Fen);
                }
            }
        }
        public BitmapImage Board {
            get {
                if (_chessBoard is null) _chessBoard = ChessImages.BoardNoPieces;
                return _chessBoard;
            }
            set {
                if (_chessBoard != value) {
                    _chessBoard = value;
                    RaisePropertyChanged(() => Board);
                }
            }
        }
    }
}
