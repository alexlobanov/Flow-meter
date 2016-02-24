using System;
using System.Diagnostics;
using System.Text;
using FlowMeterLibr.Structs;
using FlowMeterLibr.TO;

namespace FlowMeterLibr.Сommunication
{
    public static class ResponseData
    {
        private const int MinimumLengthData = 4;

        private static FlowTypeWork DetectTypeWork(this byte[] data)
        {
            byte statusByte = data[1];
            var bit6 = statusByte.GetBit(6);
            var bit7 = statusByte.GetBit(7);
            var bits = bit6.ToString() + bit7.ToString();

            var bitString = statusByte.GetBitString();
          //  var bitString = data[2].GetBitString();

            if ((bit7 == 0) && (bit6 == 0))
                return FlowTypeWork.ServiceWork;
            if ((bit7 == 0) && (bit6 == 1))
                return FlowTypeWork.NormalWork;
            if ((bit7 == 1) && (bit6 == 1))
                return FlowTypeWork.ErrorWork;
            return default(FlowTypeWork);
        }

        
        private static string ToString(this char[] arrayData)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < arrayData.Length; i++)
            {
                str.Append(arrayData[i]);
            }
            return str.ToString();
        }

        public static FlowMeterState ParseState(byte[] data)
        {
            var typeWork = data.DetectTypeWork();
            //TODO: typeWork add;
            string hex = BitConverter.ToString(data);
            Console.WriteLine(hex);
            if (data.Length <= MinimumLengthData)
                return new FlowMeterState();
            switch (data[0])
            {
                case (byte) FlowCommands.FactoryReset:
                    Debug.WriteLine("Respone FactoryReset");
                    return new FlowMeterState();

                case (byte) FlowCommands.SaveAllSettings2memory:
                    Debug.WriteLine("Respone SaveAllSettings2memory");
                    return new FlowMeterState();

                case (byte) FlowCommands.DiveceError2Usb:
                    Debug.WriteLine("Respone DiveceError2Usb");
                    return new FlowMeterState();

                case (byte) FlowCommands.MainCfg:
                    var cnfStructResived = new FlowConfigStruct(data);
                    Debug.WriteLine("Respone MainCfg");
                    return new FlowMeterState(cnfStructResived, FlowCommands.MainCfg, typeWork);

                case (byte) FlowCommands.USmetrVariablesCmd:
                    Debug.WriteLine("Respone USmetrVariablesCmd");
                    return new FlowMeterState();

                case (byte) FlowCommands.RtcTime:
                    Debug.WriteLine("Respone RtcTime");
                    var dateStructResived = new FlowDateStruct(data);
                    return new FlowMeterState(dateStructResived.ConvertedDateTime, FlowCommands.RtcTime, typeWork);

                case (byte) FlowCommands.PulseCfg:
                    Debug.WriteLine("Respone PulseCfg");
                    return new FlowMeterState();

                case (byte) FlowCommands.ModBusCfg:
                    Debug.WriteLine("Respone ModBusCfg");
                    return new FlowMeterState();

                case (byte) FlowCommands.DeviceInfo:
                    Debug.WriteLine("Respone DeviceInfo");
                    var deviceInfo = new FlowCommonDevInfo(data);
                    return new FlowMeterState(deviceInfo, FlowCommands.DeviceInfo,typeWork);

                case (byte) FlowCommands.FormatEEPROM:
                    Debug.WriteLine("Respone FormatEEPROM");
                    return new FlowMeterState();

                case (byte) FlowCommands.RunCalibrate:
                    Debug.WriteLine("Respone RunCalibrate");
                    return new FlowMeterState();

                default:
                    Debug.WriteLine("[Get] not found command : " + data[0]);
                    return new FlowMeterState();
            }
        }
    }
}