using System;
using System.Runtime.InteropServices;
using FlowMeterLibr.Сommunication;

namespace FlowMeterLibr.Structs
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Size = 13), Serializable]
    public struct PulseStruct
    {
        private byte pusleOutNbr;
        private byte pulseOutMode;
        private byte logicUnit;
        private byte pulseOutEnable;
        private byte pulseIsConfigured;

        public byte _pusleOutNbr
        {
            get
            {
                return (byte)(pusleOutNbr.ReadLastNBits(4));
            }
            set { pusleOutNbr = value; }
        }

        public byte _pulseOutMode
        {
            get { return (byte) pulseOutMode.ReadLastNBits(1); }
            set { pulseOutMode = value; }
        }

        public byte _logicUnit
        {
            get { return (byte)((logicUnit.ReadLastNBits(1))); }
            set { logicUnit = value; }
        }

        public byte _pulseOutEnable
        {
            get { return (byte)((pulseOutEnable.ReadLastNBits(1))); }
            set { pulseOutEnable = value; }
        }

        public byte _pulseIsConfigured
        {
            get { return (byte)((pulseIsConfigured.ReadLastNBits(1))); }
            set { pulseIsConfigured = value; }
        }


        public float _weightPulse;

        public ushort _maxFrequency;

        public ushort _pulseDescription;
    } 

    public class FlowPulseStruct
    {
        private PulseStruct _flowStruct;

        public FlowPulseStruct(byte[] data)
        {
            _flowStruct = data.ToStruct<PulseStruct>();
        }

        public FlowPulseStruct(PulseStruct flowStruct)
        {
            _flowStruct = flowStruct;
        }

        public PulseStruct FlowStruct
        {
            get { return _flowStruct; }
        }
    }
}