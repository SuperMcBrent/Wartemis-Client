using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Wartemis.Models;
using Wartemis.Tools;

namespace Chess.Models {
    public class ChessState : State {

        public string Fen { get; private set; }

        public ChessState(State state) : base(state.Raw, state.TimeReceived) {
            dynamic fen = JObject.Parse(Raw);
            Fen = (string)fen.fen;
        }
    }
}
