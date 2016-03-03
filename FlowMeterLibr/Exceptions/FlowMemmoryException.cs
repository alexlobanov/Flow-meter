using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowMeterLibr.Exceptions
{
    class FlowMemmoryException : Exception
    {
        public FlowMemmoryException() 
            : base("[ВНИМАНИЕ] Ошибка внешней памяти.")
        {
            //AddCustom
        }

        public FlowMemmoryException(string format, params object[] args) : base(string.Format(format, args))
        {
        }

        public FlowMemmoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public FlowMemmoryException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }
    }
}
