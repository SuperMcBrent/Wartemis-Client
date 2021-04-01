using PresentationLayer.Framework;
using PresentationLayer.Interfaces;
using System.Windows.Media;
using PresentationLayer.Models;
using PresentationLayer.Tools;

namespace PresentationLayer.ViewModels.Pages {
    public class ChessBoardViewModel : ApplicationViewModelBase, IPageViewModel {

        //private ImageSource _chessBoardImage;
        private ChessBoard _chessBoard;

        public string Name { get; private set; } = "ChessBoard";

        public ChessBoard ChessBoard {
            get {
                if (_chessBoard is null) _chessBoard = new ChessBoard();
                return _chessBoard;
            }
            set {
                if (_chessBoard != value) {
                    _chessBoard = value;
                    RaisePropertyChanged(() => ChessBoard);
                }
            }
        }

        public ChessBoardViewModel() {
            
        }
    }
}
