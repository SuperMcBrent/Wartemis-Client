using Chess.Models.Pieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Models {
    public class PieceFactory {
        public Piece GetPieceFromDesignator(string designator) {
            Color color = char.IsUpper(designator.ToCharArray()[0]) ? Color.WHITE : Color.BLACK;
            switch (designator.ToLower().ToCharArray()[0]) {
                case 'p':
                    return new Pawn(color);
                case 'n':
                    return new Knight(color);
                case 'b':
                    return new Bishop(color);
                case 'r':
                    return new Rook(color);
                case 'q':
                    return new Queen(color);
                case 'k':
                    return new King(color);
                default:
                    break;
            }
            return null;
        }
    }
}
