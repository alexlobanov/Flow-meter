using System;
using System.Diagnostics;
using System.Text;

namespace FlowMeterLibr.Structs
{
    public class FlowConfigStruct
    {
        public FlowConfigStruct()
        {
        }

        public FlowConfigStruct(byte pipeDiametr, float c0, float angle, float nullThresold, byte nbrValuesForAvg,
            byte nbrValuesForCalibrates,
            byte schemeSelect, float sensorDistance, float calibrateValue, float koeff1, float nu)
        {
            PipeDiametr = pipeDiametr;
            C0 = c0;
            Angle = angle;
            NullThresold = nullThresold;
            NbrValuesForAvg = nbrValuesForAvg;
            NbrValuesForCalibrates = nbrValuesForCalibrates;
            SchemeSelect = schemeSelect;
            SensorDistance = sensorDistance;
            CalibrateValue = calibrateValue;
            Koeff1 = koeff1;
            Nu = nu;
        }


        public FlowConfigStruct(byte[] data)
        {
            //TODO: write all data into struct
            
        }



        public byte PipeDiametr { get; set; }

        public float C0 { get; set; }

        public float Angle { get; set; }

        public float NullThresold { get; set; }

        public byte NbrValuesForAvg { get; set; }

        public byte NbrValuesForCalibrates { get; set; }

        public byte SchemeSelect { get; set; }

        public float SensorDistance { get; set; }

        public float CalibrateValue { get; set; }

        public float Koeff1 { get; set; }

        public float Nu { get; set; }



        public override string ToString()
        {
            throw new Exception("No init");
        }
    }
}