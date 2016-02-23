using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlowMeterLibr.TO;
using HidLibrary;

namespace FlowMeterLibr.Requests
{
    class ConfigRequest : Requets
    {
        public ConfigRequest(ref HidDevice device) : 
            base(
                FlowIdParkage.DataOutId,
                FlowCommands.MainCfg,
                FlowStatusRequest.Get)
            {
            var data = base.getDataToSend();
            var report = new HidReport(data.Length, new HidDeviceData(data, HidDeviceData.ReadStatus.Success));
            device.WriteReport(report,1000);
        }
    }
}
