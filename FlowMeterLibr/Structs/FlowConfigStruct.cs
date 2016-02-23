using System;
using System.Diagnostics;
using System.Text;
using FlowMeterLibr.Сommunication;

namespace FlowMeterLibr.Structs
{
    public struct ConfigStruct
    {
        float pipeDiamer;
        float CO;
        float angle;
        float nullThresold;
        byte nbrValuesForAvg;
        byte nbrValuesForCalibrates;
        byte schemeSelect;
        float sensorDistance;
        float calibraeValue;
        float koeff1;
        float nu;

    }

    public class FlowConfigStruct
    {
        private ConfigStruct _flowStruct;

        public FlowConfigStruct(byte[] data)
        {
            _flowStruct =  ConverterData.ToStruct<ConfigStruct>(data);
        }



        public override string ToString()
        {
            throw new Exception("No init");
        }
    }
}