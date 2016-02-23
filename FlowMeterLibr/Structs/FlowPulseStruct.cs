namespace FlowMeterLibr.Structs
{
    public class FlowPulseStruct
    {
        public FlowPulseStruct(byte pusleOutNbr, byte pulseOutMode, byte logicUnit,
            byte pulseOutEnable, byte pulseIsConfigured,
            float weightPulse, ushort maxFrequency, ushort pulseDescription)
        {
            PusleOutNbr = pusleOutNbr;
            PulseOutMode = pulseOutMode;
            LogicUnit = logicUnit;
            PulseOutEnable = pulseOutEnable;
            PulseIsConfigured = pulseIsConfigured;
            WeightPulse = weightPulse;
            MaxFrequency = maxFrequency;
            PulseDescription = pulseDescription;
        }

        public byte PusleOutNbr { get; set; }

        public byte PulseOutMode { get; set; }

        public byte LogicUnit { get; set; }

        public byte PulseOutEnable { get; set; }

        public byte PulseIsConfigured { get; set; }

        public float WeightPulse { get; set; }

        public ushort MaxFrequency { get; set; }

        public ushort PulseDescription { get; set; }
    }
}