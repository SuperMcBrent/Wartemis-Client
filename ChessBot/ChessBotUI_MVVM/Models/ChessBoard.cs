using PresentationLayer.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models {
    [Serializable]
    public class ChessBoard : Notifier {

        private List<string> _cells;

        public ChessBoard(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1") {
            Cells = new List<string>();
            for (int i = 0; i < 64; i++) {
                Cells.Add("");
            }
            DeconstructFen(fen);
        }

        public List<string> Cells {
            get => _cells;
            set {
                if (_cells != value) {
                    _cells = value;
                    RaisePropertyChanged(() => Cells);
                }
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
                        Cells[counter] = part.ToString();
                        counter++;
                    }
                }
            }

            string colorPlaying = fen.Split(' ')[1];
        }
    }
}
