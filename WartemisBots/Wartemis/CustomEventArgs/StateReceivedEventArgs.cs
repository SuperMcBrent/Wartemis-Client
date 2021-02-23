using System;
using System.Collections.Generic;
using System.Text;

namespace Wartemis.CustomEventArgs {
    public class StateReceivedEventArgs {
        public DateTime TimeReached { get; private set; }
        public string State { get; private set; }

        public StateReceivedEventArgs(string state) {
            TimeReached = DateTime.Now;
            State = state;
        }
    }
}
