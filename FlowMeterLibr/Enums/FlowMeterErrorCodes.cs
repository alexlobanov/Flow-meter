using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowMeterLibr.Enums
{
    public enum FlowMeterErrorCodes
    {
        DeviceCrash = 0,
        LowBattery = 1,
        tubeInAir = 2,
        NoUltrasonicSensors = 3,
        NoInSetupMode = 7,
        ErrorInExternalMemory = 31
    }
}
