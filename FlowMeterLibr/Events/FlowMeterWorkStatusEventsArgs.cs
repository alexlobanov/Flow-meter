using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlowMeterLibr.TO;

namespace FlowMeterLibr.Events
{
    public class FlowMeterWorkStatusEventsArgs : EventArgs
    {
        private FlowTypeWork _typeWork;

        public FlowMeterWorkStatusEventsArgs(FlowTypeWork typeWork)
        {
            _typeWork = typeWork;
        }

        public FlowTypeWork TypeWork
        {
            get { return _typeWork; }
            set { _typeWork = value; }
        }
    }
}
