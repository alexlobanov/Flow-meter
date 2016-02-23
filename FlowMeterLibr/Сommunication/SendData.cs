using System;
using FlowMeterLibr.Requests;
using FlowMeterLibr.TO;
using HidLibrary;

namespace FlowMeterLibr.Sending
{
    public static class SendData
    {        
        public static Requets SendDataToDevice(FlowCommands command,ref HidDevice device)
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
                    return CreateConfigRequest(ref device);

                case FlowCommands.USmetrVariablesCmd:
                    break;

                case FlowCommands.RtcTime:
                    return CreateDateRequest(ref device);

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
                    throw new ArgumentOutOfRangeException(nameof(command), command, null);
            }
            return default(Requets);
        }

        public static Requets SendDataToDevice(FlowCommands commands, ref HidDevice device, byte[] data)
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
                    break;
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

        private static ConfigRequest CreateConfigRequest(ref HidDevice device)
        {
            return new ConfigRequest(ref device);
        }

        private static DateRequest CreateDateRequest(ref HidDevice device)
        {
            return new DateRequest(ref device);
        }
    }
}
