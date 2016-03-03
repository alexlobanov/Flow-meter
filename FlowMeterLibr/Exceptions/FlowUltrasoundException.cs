using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowMeterLibr.Exceptions
{
    class FlowUltrasoundException : Exception
    {
        public FlowUltrasoundException() 
            : base("[ВНИМАНИЕ] Нет ультразвуковых датчиков.")
        {
            //AddCustom
        }

        public FlowUltrasoundException(string format, params object[] args) : base(string.Format(format, args))
        {
        }

        public FlowUltrasoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public FlowUltrasoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }
    }
}
