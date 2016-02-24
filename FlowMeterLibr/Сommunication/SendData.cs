using System;
using FlowMeterLibr.Requests;
using FlowMeterLibr.TO;
using HidLibrary;

namespace FlowMeterLibr.Сommunication
{
    public static class SendData
    {
        public static Requets SendDataToDevice(this HidDevice device, FlowCommands command)
        {
            switch (command)
            {
                case FlowCommands.FactoryReset:
                    break;

                case FlowCommands.SaveAllSettings2memory:
                    break;

                case FlowCommands.DiveceError2Usb:
                    break;

                case FlowCommands.MainCfg:
                    return new ConfigRequest(ref device);

                case FlowCommands.USmetrVariablesCmd:
                    break;

                case FlowCommands.RtcTime:
                    return new DateRequest(ref device);

                case FlowCommands.PulseCfg:
                    break;

                case FlowCommands.ModBusCfg:
                    break;

                case FlowCommands.DeviceInfo:
                    return new ComonDevInfoRequest(ref device,FlowStatusRequest.Set);
                case FlowCommands.DeviceInfoStop:
                    return new ComonDevInfoRequest(ref device, FlowStatusRequest.Get);
                case FlowCommands.FormatEEPROM:
                    break;

                case FlowCommands.RunCalibrate:
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(command), command, null);
            }
            return default(Requets);
        }

        public static Requets SendDataToDevice<T>(this HidDevice device, FlowCommands commands, T structToSend) 
        {
            switch (commands)
            {
                case FlowCommands.FactoryReset:
                    break;
                case FlowCommands.SaveAllSettings2memory:
                    break;
                case FlowCommands.DiveceError2Usb:
                    break;
                case FlowCommands.MainCfg:
                    break;
                case FlowCommands.USmetrVariablesCmd:
                    break;
                case FlowCommands.RtcTime:
                    return new DateRequest(ref device, structToSend);
                case FlowCommands.PulseCfg:
                    break;
                case FlowCommands.ModBusCfg:
                    break;
                case FlowCommands.DeviceInfo:
                    break;
                case FlowCommands.FormatEEPROM:
                    break;
                case FlowCommands.RunCalibrate:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(commands), commands, null);
            }
            return default(Requets);
        }
    }
}