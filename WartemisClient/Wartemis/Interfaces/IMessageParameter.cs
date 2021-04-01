using System;
using System.Collections.Generic;
using System.Text;

namespace Wartemis.Interfaces {
    public interface IMessageParameter {
        string Name { get; }
        object Value { get; }
        string GetJSONPartial();
    }
}
