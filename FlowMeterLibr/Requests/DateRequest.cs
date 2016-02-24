using FlowMeterLibr.Structs;
using FlowMeterLibr.TO;
using HidLibrary;
using FlowMeterLibr;

namespace FlowMeterLibr.Requests
{
    internal class DateRequest : Requets
    {
        public DateRequest(ref HidDevice device) :
            base(
            FlowIdParkage.DataOutId,
            FlowCommands.RtcTime,
            FlowStatusRequest.Get
            )
        {
            var data = getDataToSend();
            var report = new HidReport(data.Length, new HidDeviceData(data, HidDeviceData.ReadStatus.Success));
            device.WriteReport(report, 1000);
        }

        public DateRequest(ref HidDevice device, object dateStruct) :
            base(
            FlowIdParkage.DataOutId,
            FlowCommands.RtcTime,
            FlowStatusRequest.Set,
            ((FlowDateStruct)dateStruct).FlowStruct.GetBytes()
                )
        {
            var data = getDataToSend();
            var report = new HidReport(data.Length, new HidDeviceData(data, HidDeviceData.ReadStatus.Success));
            device.WriteReport(report, 1000);
        }
    }
}