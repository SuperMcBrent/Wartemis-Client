using System;
using System.Collections.Generic;
using System.Text;
using Wartemis.Tools;

namespace Wartemis.Models {
    public class State {

        public DateTime TimeReceived { get; private set; }
        public string Raw { get; private set; }

        public State(string message) {
            TimeReceived = DateTime.Now;
            Raw = message;
        }

        protected State(string message, DateTime received) : this(message){
            TimeReceived = received;
        }

        public override string ToString() {
            return Raw.Replace(Environment.NewLine, "").Replace(" ", "").Truncate(75);
        }

    }
}
