namespace FlowMeterLibr.Structs
{
    public class FlowModBusSctruct
    {
        public FlowModBusSctruct(byte mbMode, byte mbParityMode, byte mbBaudRate, byte mbSlaveAdress, byte mbUcPort)
        {
            MbMode = mbMode;
            MbParityMode = mbParityMode;
            MbBaudRate = mbBaudRate;
            MbSlaveAdress = mbSlaveAdress;
            MbUcPort = mbUcPort;
        }

        public FlowModBusSctruct()
        {
        }

        public byte MbMode { get; set; }

        public byte MbParityMode { get; set; }

        public byte MbBaudRate { get; set; }

        public byte MbSlaveAdress { get; set; }

        public byte MbUcPort { get; set; }
    }
}