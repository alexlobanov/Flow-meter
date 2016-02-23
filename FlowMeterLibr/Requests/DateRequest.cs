using FlowMeterLibr.TO;
using HidLibrary;

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
    }
}