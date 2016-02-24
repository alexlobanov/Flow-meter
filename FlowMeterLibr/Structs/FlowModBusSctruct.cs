using FlowMeterLibr.Сommunication;

namespace FlowMeterLibr.Structs
{
    public struct BusStruct
    {
        public byte MbMode { get; set; }

        public byte MbParityMode { get; set; }

        public byte MbBaudRate { get; set; }

        public byte MbSlaveAdress { get; set; }

        public byte MbUcPort { get; set; }
    }

    public class FlowModBusSctruct
    {
        private BusStruct _flowStruct;

        public FlowModBusSctruct(byte[] data)
        {
            _flowStruct = data.ToStruct<BusStruct>();
        }

        public FlowModBusSctruct(BusStruct flowStruct)
        {
            this._flowStruct = flowStruct;
        }

        public BusStruct FlowStruct
        {
            get { return _flowStruct; }
        }
    }
}