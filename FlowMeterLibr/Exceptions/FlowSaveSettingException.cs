using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowMeterLibr.Exceptions
{
    class FlowSaveSettingException : Exception
    {
        public FlowSaveSettingException() 
            : base("[ВНИМАНИЕ] Нет прав доступа на изменение параметра (Прибор не в режиме настройки")
        {
            //AddCustom
        }

        public FlowSaveSettingException(string format, params object[] args) : base(string.Format(format, args))
        {
        }

        public FlowSaveSettingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public FlowSaveSettingException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }
    }
}
