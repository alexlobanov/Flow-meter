using System;

namespace FlowMeterLibr
{
    public class FlowMeterEventArgs : EventArgs
    {
        public FlowMeterEventArgs(FlowMeterState state)
        {
            State = state;
        }

        public FlowMeterState State { get; private set; }
    }
}