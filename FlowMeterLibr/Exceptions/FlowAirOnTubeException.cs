using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowMeterLibr.Exceptions
{
    class FlowAirOnTubeException : Exception
    {
        public FlowAirOnTubeException() 
            : base("[ВНИМАНИЕ] Низкое напряжение батареи.")
        { 
            //AddCustom
        }

        public FlowAirOnTubeException(string format, params object[] args) : base(string.Format(format, args))
        {
        }

        public FlowAirOnTubeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public FlowAirOnTubeException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }
    }
}
