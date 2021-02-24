using Chess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Models.Pieces {
    public abstract class Piece {

        private List<Piece> _endangeredBy;
        private List<Piece> _endangering;
        private List<Piece> _protectedBy;
        private List<Piece> _protecting;

        public Color Color { get; private set; }
        public string Designator { get; protected set; }
        public Cell Cell { get; set; }

        public List<Cell> AvailableTargets { get; private set; }
        public List<Cell> UnAvailableTargets { get; private set; }

        public IReadOnlyList<Piece> EndangeredBy {
            get => _endangeredBy.AsReadOnly();
            private set => _endangeredBy = (List<Piece>)value;
        }
        public IReadOnlyList<Piece> Endangering { 
            get => _endangering.AsReadOnly(); 
            private set => _endangering = (List<Piece>)value; 
        }
        public IReadOnlyList<Piece> ProtectedBy { 
            get => _protectedBy.AsReadOnly(); 
            private set => _protectedBy = (List<Piece>)value; 
        }
        public IReadOnlyList<Piece> Protecting { 
            get => _protecting.AsReadOnly(); 
            private set => _protecting = (List<Piece>)value; 
        }

        public Piece(Color color) {
            Color = color;
            AvailableTargets = new List<Cell>();
            UnAvailableTargets = new List<Cell>();
            EndangeredBy = new List<Piece>();
            ProtectedBy = new List<Piece>();
            Protecting = new List<Piece>();
        }

        public void AddToProtectedBy(Piece piece) {
            if (!_protectedBy.Contains(piece)) _protectedBy.Add(piece);
        }

        public void AddToProtecting(Piece piece) {
            if (!_protecting.Contains(piece)) _protecting.Add(piece);
        }

        public void AddToEndangeredBy(Piece piece) {
            if (!_endangeredBy.Contains(piece)) _endangeredBy.Add(piece);
        }

        public void AddToEndangering(Piece piece) {
            if (!_endangering.Contains(piece)) _endangering.Add(piece);
        }

        public abstract void Evaluate();

        protected void AddToAvailableOrUnavailableTargets(Cell cell) {
            if (cell.IsEmpty()) {
                AvailableTargets.Add(cell);
            } else {
                UnAvailableTargets.Add(cell);
            }
        }

        protected void AddToProtectingOrEndangeringPieces(Cell cell) {
            if (cell.Piece.Color.Equals(Color)) {
                // Attack position is ally
                cell.Piece.AddToProtectedBy(this);
                AddToProtecting(cell.Piece);
            } else {
                // Attack position is enemy
                cell.Piece.AddToEndangeredBy(this);
                AddToEndangering(cell.Piece);
            }
        }

        public override string ToString() {
            return Color.Equals(Color.BLACK) ? Designator : Designator.ToUpper();
        }
    }
}
