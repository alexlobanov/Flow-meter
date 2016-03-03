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
    internal class ModBusRequest : Requets
    {
        public ModBusRequest(ref HidDevice device, object dateStruct) :
            base(
            FlowIdParkage.DataOutId,
            FlowCommands.ModBusCfg,
            FlowStatusRequest.Set, 
            ((BusStruct)dateStruct).GetBytes()
                )
        {
            var data = getDataToSend();
            var report = new HidReport(data.Length, new HidDeviceData(data, HidDeviceData.ReadStatus.Success));
            device.WriteReport(report, 1000);
        }

        public ModBusRequest(ref HidDevice device) : 
            base(
                FlowIdParkage.DataOutId,
                FlowCommands.ModBusCfg,
                FlowStatusRequest.Get)
        {
            var data = getDataToSend();
            var report = new HidReport(data.Length, new HidDeviceData(data, HidDeviceData.ReadStatus.Success));
            device.WriteReport(report, 1000);
        }
    }
}
