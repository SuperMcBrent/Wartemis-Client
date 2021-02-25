using System;
using System.Collections.Generic;
using System.Text;

namespace Wartemis.Models {
    public class Action {

        public DateTime TimeSent { get; private set; }
        public string Raw { get; protected set; }

        public Action() {
            TimeSent = DateTime.Now;
        }
    }
}
