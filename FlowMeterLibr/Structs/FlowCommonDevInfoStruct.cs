using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using FlowMeterLibr.Сommunication;

namespace FlowMeterLibr.Structs
{
     
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Size =  54), Serializable]
    public struct DevInfoStruct
    {

        public float QCurrent1; //4

        public double VModule1;//8

        public double VPlus1;//8

        public double VMinus1;//8

        public UInt32 TpTime1;//4

        public UInt32 TeTime1; //4

        public UInt32 DeviceCrc1; //4

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]

        public string FirmwareName; //17??

        public UInt32 DeviceSerial; //4
    }

    public class FlowCommonDevInfo
    {
        private DevInfoStruct _flowStruct;

        public FlowCommonDevInfo(byte[] data)
        {
            _flowStruct = data.ToStruct<DevInfoStruct>();
        }

        public FlowCommonDevInfo(DevInfoStruct flowStruct)
        {
            _flowStruct = flowStruct;
        }

        public DevInfoStruct FlowStruct
        {
            get { return _flowStruct; }
        }


    }
}