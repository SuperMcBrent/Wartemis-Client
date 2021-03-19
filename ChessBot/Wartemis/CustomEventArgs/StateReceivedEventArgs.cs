using System;
using System.Collections.Generic;
using System.Text;
using Wartemis.Models;

namespace Wartemis.CustomEventArgs {
    public class StateReceivedEventArgs {
        public DateTime TimeReached { get; private set; }
        public State State { get; private set; }

        public StateReceivedEventArgs(State state) {
            TimeReached = DateTime.Now;
            State = state;
        }
    }
}
