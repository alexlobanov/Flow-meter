namespace FlowMeterLibr.Structs
{
    public class FlowCommonDevInfo
    {
        public FlowCommonDevInfo(float qCurrent, double vModule, double vPlus, double vMinus, uint tpTime, uint teTime,
            uint deviceCrc, string firmwareName, uint deviceSerial)
        {
            QCurrent1 = qCurrent;
            VModule1 = vModule;
            VPlus1 = vPlus;
            VMinus1 = vMinus;
            TpTime1 = tpTime;
            TeTime1 = teTime;
            DeviceCrc1 = deviceCrc;
            FirmwareName = firmwareName;
            DeviceSerial = deviceSerial;
        }

        public FlowCommonDevInfo()
        {
        }

        public float QCurrent1 { get; set; }

        public double VModule1 { get; set; }

        public double VPlus1 { get; set; }

        public double VMinus1 { get; set; }

        public uint TpTime1 { get; set; }

        public uint TeTime1 { get; set; }

        public uint DeviceCrc1 { get; set; }

        public string FirmwareName { get; set; }

        public uint DeviceSerial { get; set; }
    }
}