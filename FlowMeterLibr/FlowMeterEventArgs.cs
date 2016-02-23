using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowMeterLibr
{
    public class FlowMeterEventArgs : EventArgs
    {
        public FlowMeterState State { get; private set; }
        public FlowMeterEventArgs(FlowMeterState state)
        {
            State = state;
        }
    }
}
