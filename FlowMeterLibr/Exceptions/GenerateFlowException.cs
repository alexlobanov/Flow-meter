using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using FlowMeterLibr.Enums;

namespace FlowMeterLibr.Exceptions
{
    public static class GenerateFlowException
    {
        public static void Generate(byte[] data)
        {
            var indexError = -1;
            var dataWithExeptions = new byte[4];
            Array.Copy(data,3,dataWithExeptions,0,data.Length - 3);
            var bitString = dataWithExeptions.GetBitString();
            for (var i = 0; i < bitString.Length; i++)
            {
                if (bitString[i] == 1)
                {
                    indexError = i;
                    break;
                }
            }
            if (indexError == -1)
            {
                Debug.WriteLine("[GenareteFlowEx] Исключения не найденны");
                return;
            }
            var foo = (FlowMeterErrorCodes)indexError;
            switch (foo)
            {
                case FlowMeterErrorCodes.DeviceCrash:
                    throw new FlowDeviceInvalidException();

                case FlowMeterErrorCodes.LowBattery:
                    throw new FlowLowBatteryException();

                case FlowMeterErrorCodes.tubeInAir:
                    throw new FlowAirOnTubeException();

                case FlowMeterErrorCodes.NoUltrasonicSensors:
                    throw new FlowUltrasoundException();

                case FlowMeterErrorCodes.NoInSetupMode:
                    throw new FlowSaveSettingException();

                case FlowMeterErrorCodes.ErrorInExternalMemory:
                    throw new FlowMemmoryException();
            }
        }
    }
}
