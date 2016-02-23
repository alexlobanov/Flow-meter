using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowMeterLibr.Structs
{
    public class FlowCommonDevInfo
    {
        private float QCurrent;
        private double VModule;
        private double VPlus;
        private double VMinus;
        private uint TpTime;
        private uint TeTime;
        private uint DeviceCrc;
        private string firmwareName;
        private uint deviceSerial;

        public FlowCommonDevInfo(float qCurrent, double vModule, double vPlus, double vMinus, uint tpTime, uint teTime, uint deviceCrc, string firmwareName, uint deviceSerial)
        {
            QCurrent = qCurrent;
            VModule = vModule;
            VPlus = vPlus;
            VMinus = vMinus;
            TpTime = tpTime;
            TeTime = teTime;
            DeviceCrc = deviceCrc;
            this.firmwareName = firmwareName;
            this.deviceSerial = deviceSerial;
        }

        public FlowCommonDevInfo()
        {
        }

        public float QCurrent1
        {
            get { return QCurrent; }
            set { QCurrent = value; }
        }

        public double VModule1
        {
            get { return VModule; }
            set { VModule = value; }
        }

        public double VPlus1
        {
            get { return VPlus; }
            set { VPlus = value; }
        }

        public double VMinus1
        {
            get { return VMinus; }
            set { VMinus = value; }
        }

        public uint TpTime1
        {
            get { return TpTime; }
            set { TpTime = value; }
        }

        public uint TeTime1
        {
            get { return TeTime; }
            set { TeTime = value; }
        }

        public uint DeviceCrc1
        {
            get { return DeviceCrc; }
            set { DeviceCrc = value; }
        }

        public string FirmwareName
        {
            get { return firmwareName; }
            set { firmwareName = value; }
        }

        public uint DeviceSerial
        {
            get { return deviceSerial; }
            set { deviceSerial = value; }
        }
    }
}
