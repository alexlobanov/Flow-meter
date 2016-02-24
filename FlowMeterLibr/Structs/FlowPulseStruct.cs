using FlowMeterLibr.Сommunication;

namespace FlowMeterLibr.Structs
{
    public struct PulseStruct
    {
        byte _pusleOutNbr;
        byte _pulseOutMode;
        byte _logicUnit;
        byte _pulseOutEnable;
        byte _pulseIsConfigured;
        float _weightPulse;
        ushort _maxFrequency;
        ushort _pulseDescription;
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