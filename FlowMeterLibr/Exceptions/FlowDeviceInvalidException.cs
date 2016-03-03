using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowMeterLibr.Exceptions
{
    class FlowDeviceInvalidException : Exception
    {
        public FlowDeviceInvalidException() 
            : base("[ВНИМАНИЕ] Аппаратная неисправность прибора.")
        {
            //AddCustom
        }

        public FlowDeviceInvalidException(string format, params object[] args) : base(string.Format(format, args))
        {
        }

        public FlowDeviceInvalidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public FlowDeviceInvalidException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }
    }
}
