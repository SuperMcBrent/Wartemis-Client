using System;
using System.Collections.Generic;
using System.Text;
using Wartemis.Interfaces;

namespace Wartemis.Models {
    class IntParameter : IMessageParameter {
        public string Name { get; } = "undefined";
        public object Value { get; }

        public IntParameter(string name, int value) {
            this.Name = name;
            this.Value = (int)value;
        }

        public string GetJSONPartial() {
            return $"\"{this.Name}\":{this.Value}";
        }
    }
}
