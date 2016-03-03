using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using FlowMeterLibr.Сommunication;

namespace FlowMeterLibr.Structs
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Size = 35), Serializable]
    public struct ConfigStruct
    {
        public float pipeDiamer; //4
        public float CO; //4
        public float angle; //4
        public float nullThresold; //4 
        public byte nbrValuesForAvg; //1
        public byte nbrValuesForCalibrates; //1
        public byte schemeSelect; //1
        public float sensorDistance; //4
        public float calibraeValue;//4
        public float koeff1;//4
        public float nu;//4

    }

    public class FlowConfigStruct
    {

        public FlowConfigStruct(byte[] data)
        {
            GetConfigStruct =  data.ToStruct<ConfigStruct>();
        }

        public override string ToString()
        {
            throw new Exception("No init");
        }

        public FlowConfigStruct(ConfigStruct flowStruct)
        {
            GetConfigStruct = flowStruct;
        }

        public ConfigStruct GetConfigStruct { get; set; }
    }
}