using System;
using System.Collections.Generic;
using System.Text;
using Wartemis.Models;

namespace TicTacToe.Models {
    public class TicTacToeBot : Bot {
        public TicTacToeBot(string name) : base(name, GameType.TicTacToe) {

        }
    }
}
