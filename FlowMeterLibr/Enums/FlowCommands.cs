using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowMeterLibr.TO
{
    public enum FlowCommands
    {
        FactoryReset = 2,
        SaveAllSettings2memory = 3,
        DiveceError2Usb = 4,
        MainCfg = 5,
        USmetrVariablesCmd = 10,
        RtcTime = 12,
        PulseCfg = 13,
        ModBusCfg = 15,
        DeviceInfo = 16,
        FormatEEPROM = 6,
        RunCalibrate = 7,
    }
}
