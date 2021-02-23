using Chess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Models.Pieces {
    public abstract class Piece {

        public Color Color { get; private set; }
        public string Designator { get; protected set; }

        public List<Cell> AvailableTarget { get; private set; }
        public List<Cell> UnAvailableTarget { get; private set; }
        public List<Cell> SpookyScaryCells { get; private set; }
        public List<Cell> Protectors { get; private set; }

        public Piece(Color color) {
            Color = color;
            AvailableTarget = new List<Cell>();
            UnAvailableTarget = new List<Cell>();
            SpookyScaryCells = new List<Cell>();
            Protectors = new List<Cell>();
        }

        public override string ToString() {
            return Color.Equals(Color.BLACK) ? Designator : Designator.ToUpper();
        }
    }
}
