using System;
using System.Collections.Generic;
using System.Text;
using Wartemis.Interfaces;

namespace Wartemis.Models {
    class UnformattedParameter : IMessageParameter {
        public string Name { get; } = "undefined";
        public object Value { get; }

        public UnformattedParameter(string name, string value) {
            this.Name = name;
            this.Value = (string)value;
        }

        public string GetJSONPartial() {
            return $"\"{this.Name}\":{this.Value}\"";
        }
    }
}
