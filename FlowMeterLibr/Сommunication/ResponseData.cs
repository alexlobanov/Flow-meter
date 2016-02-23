using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlowMeterLibr.Structs;
using FlowMeterLibr.TO;

namespace FlowMeterLibr.Сommunication
{
    public static class ResponseData
    {
        private const int MinimumLengthData = 4;


        public static FlowMeterState ParseState(byte[] data)
        {
            if (data.Length <= MinimumLengthData)
                return new FlowMeterState();
            switch (data[0])
            {
                case (byte)FlowCommands.FactoryReset:
                    Debug.WriteLine("Respone FactoryReset");
                    return new FlowMeterState();

                case (byte)FlowCommands.SaveAllSettings2memory: 
                    Debug.WriteLine("Respone SaveAllSettings2memory");
                    return new FlowMeterState();

                case (byte)FlowCommands.DiveceError2Usb:
                    Debug.WriteLine("Respone DiveceError2Usb");
                    return new FlowMeterState();

                case (byte)FlowCommands.MainCfg:
                    var CnfStructResived = new FlowConfigStruct(data);
                    Debug.WriteLine("Respone MainCfg");
                    return new FlowMeterState(CnfStructResived, FlowCommands.MainCfg);

                case (byte)FlowCommands.USmetrVariablesCmd:
                    Debug.WriteLine("Respone USmetrVariablesCmd");
                    return new FlowMeterState();

                case (byte)FlowCommands.RtcTime:
                    Debug.WriteLine("Respone RtcTime");
                    var dateStructResived = new FlowDateStruct(data);
                    return new FlowMeterState(dateStructResived.GetDateTime(), FlowCommands.RtcTime);

                case (byte)FlowCommands.PulseCfg:
                    Debug.WriteLine("Respone PulseCfg");
                    return new FlowMeterState();

                case (byte)FlowCommands.ModBusCfg:
                    Debug.WriteLine("Respone ModBusCfg");
                    return new FlowMeterState();

                case (byte)FlowCommands.DeviceInfo:
                    Debug.WriteLine("Respone DeviceInfo");
                    return new FlowMeterState();

                case (byte)FlowCommands.FormatEEPROM:
                    Debug.WriteLine("Respone FormatEEPROM");
                    return new FlowMeterState();

                case (byte)FlowCommands.RunCalibrate:
                    Debug.WriteLine("Respone RunCalibrate");
                    return new FlowMeterState();

                default:
                    throw new ArgumentOutOfRangeException();

            }
        }


        public static void GenerateEvents(FlowMeterState state)
        {
            switch (state.UsingCommand)
            {
                case FlowCommands.FactoryReset:
                    Debug.WriteLine("FactoryReset event");
                    break;
                case FlowCommands.SaveAllSettings2memory:
                    Debug.WriteLine("SaveAllSettings2memory event");
                    break;
                case FlowCommands.DiveceError2Usb:
                    Debug.WriteLine("DiveceError2Usb event");
                    break;
                case FlowCommands.MainCfg:
                    OnGetConfig(state);
                    Debug.WriteLine("MainCfg event");
                    break;
                case FlowCommands.USmetrVariablesCmd:
                    break;
                case FlowCommands.RtcTime:
                    Debug.WriteLine("RtcTime event");
                    OnTimeChange(state);
                    break;
                case FlowCommands.PulseCfg:
                    Debug.WriteLine("PulseCfg event");
                    break;
                case FlowCommands.ModBusCfg:
                    Debug.WriteLine("ModBusCfg event");
                    break;
                case FlowCommands.DeviceInfo:
                    Debug.WriteLine("DeviceInfo event");
                    break;
                case FlowCommands.FormatEEPROM:
                    Debug.WriteLine("FormatEEPROM event");
                    break;
                case FlowCommands.RunCalibrate:
                    Debug.WriteLine("RunCalibrate event");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            prevState = state;
        }
    }
}
