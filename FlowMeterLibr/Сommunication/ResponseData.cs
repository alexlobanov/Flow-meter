using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FlowMeterLibr.Exceptions;
using FlowMeterLibr.Structs;
using FlowMeterLibr.TO;

namespace FlowMeterLibr.Сommunication
{
    public static class ResponseData
    {
        private const int MinimumLengthData = 3;

        private static FlowTypeWork DetectTypeWork(this byte[] data)
        {
            byte statusByte = data[1];
            var bit6 = statusByte.GetBit(6);
            var bit7 = statusByte.GetBit(7);
            if ((bit7 == 0) && (bit6 == 0))
                return FlowTypeWork.ServiceWork;
            if ((bit7 == 0) && (bit6 == 1))
                return FlowTypeWork.NormalWork;
            if ((bit7 == 1) && (bit6 == 1))
                return FlowTypeWork.ErrorWork;
            return default(FlowTypeWork);
        }

       

        public static FlowMeterState ParseState(byte[] data)
        {
            var typeWork = data.DetectTypeWork(); //определяем в каком режиме работает устройство.
            if (data.isZeroOrEmpry() || (data.Length <= MinimumLengthData))
                //длина меньше min - значит ответ пустой(или возможно помеха). 
                //(isZeroOrEmpry == true) - одни нули или пустой массив. (описание функции в FlowExtensions.cs)
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
                    Debug.WriteLine("[ERROR!!!!]Respone DiveceError2Usb");
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
                    var pulseStruct = new FlowPulseStruct(data);
                    Console.WriteLine(data.GetBitString().BinaryStringToHexString());
                    return new FlowMeterState(pulseStruct, FlowCommands.PulseCfg, typeWork);

                case (byte) FlowCommands.ModBusCfg:
                    Debug.WriteLine("Respone ModBusCfg");
                   // Console.WriteLine(data.GetBitString().BinaryStringToHexString());
                    var modBusStruct = new FlowModBusSctruct(data);
                    return new FlowMeterState(modBusStruct, FlowCommands.ModBusCfg, typeWork);

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
                    GenerateFlowException.Generate(data);
                    return new FlowMeterState();
            }
        }
    }
}