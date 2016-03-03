using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowMeterLibr.Exceptions
{
    class FlowLowBatteryException : Exception
    {
        public FlowLowBatteryException() 
            : base("[ВНИМАНИЕ] Низкое напряжение батареи.")
        {
            //AddCustom
        }

        public FlowLowBatteryException(string format, params object[] args) : base(string.Format(format, args))
        {
        }

        public FlowLowBatteryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public FlowLowBatteryException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }
    }
}
