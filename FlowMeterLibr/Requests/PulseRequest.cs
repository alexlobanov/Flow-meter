using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlowMeterLibr.Structs;
using FlowMeterLibr.TO;
using HidLibrary;

namespace FlowMeterLibr.Requests
{
    class PulseRequest : Requets
    {
        public PulseRequest(ref HidDevice device, object dateStruct) :
            base(
            FlowIdParkage.DataOutId,
            FlowCommands.PulseCfg,
            FlowStatusRequest.Set, 
            ((PulseStruct)dateStruct).GetBytes()
                )
        {
            var data = getDataToSend();
            var report = new HidReport(data.Length, new HidDeviceData(data, HidDeviceData.ReadStatus.Success));
            device.WriteReport(report, 1000);
        }

        public PulseRequest(ref HidDevice device) : 
            base(
                FlowIdParkage.DataOutId,
                FlowCommands.PulseCfg,
                FlowStatusRequest.Get)
        {
            var data = getDataToSend();
            var report = new HidReport(data.Length, new HidDeviceData(data, HidDeviceData.ReadStatus.Success));
            device.WriteReport(report, 1000);
        }
    }
}
