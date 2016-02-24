using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlowMeterLibr.TO;
using HidLibrary;

namespace FlowMeterLibr.Requests
{
    class ComonDevInfoRequest : Requets
    {
        public ComonDevInfoRequest(ref HidDevice device, FlowStatusRequest flowStatusRequest) 
            : base(
                  FlowIdParkage.DataOutId, 
                  FlowCommands.DeviceInfo,
                  flowStatusRequest //Потоковая передача данных
                  )
        {
            var data = getDataToSend();
            var report = new HidReport(data.Length, new HidDeviceData(data, HidDeviceData.ReadStatus.Success));
            device.WriteReport(report, 1000);
        }
    }
}
