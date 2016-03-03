using System;
using System.Runtime.InteropServices;
using FlowMeterLibr.Сommunication;

namespace FlowMeterLibr.Structs
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Size = 5), Serializable]
    public struct BusStruct
    {
        private byte _MbMode;

        private byte _MbParityMode;

        private byte _MbBaudRate;

        public byte MbSlaveAdress;

        public byte MbUcPort;

        public byte MbMode
        {
            get
            {
                return _MbMode.ReadLastNBits(2); 
                
            }
            set { _MbMode = value; }
        }

        public byte MbParityMode
        {
            get
            {
                return _MbParityMode.ReadLastNBits(2); 
                
            }
            set { _MbParityMode = value; }
        }

        public byte MbBaudRate
        {
            get { return _MbBaudRate.ReadLastNBits(3); }
            set { _MbBaudRate = value; }
        }
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