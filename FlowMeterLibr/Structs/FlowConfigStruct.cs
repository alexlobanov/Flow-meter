using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowMeterLibr.Structs
{
    public class FlowConfigStruct
    {
        private byte _pipeDiametr;
        private float _c0;
        private float _angle;
        private float _nullThresold;
        private byte _nbrValuesForAvg;
        private byte _nbrValuesForCalibrates;
        private byte _schemeSelect;
        private float _sensorDistance;
        private float _calibrateValue;
        private float _koeff1;
        private float _nu;
        
        public FlowConfigStruct()
        {

        }

        public FlowConfigStruct(byte pipeDiametr, float c0, float angle, float nullThresold, byte nbrValuesForAvg, byte nbrValuesForCalibrates, 
            byte schemeSelect, float sensorDistance, float calibrateValue, float koeff1, float nu, string withoutParse)
        {
            _pipeDiametr = pipeDiametr;
            _c0 = c0;
            _angle = angle;
            _nullThresold = nullThresold;
            _nbrValuesForAvg = nbrValuesForAvg;
            _nbrValuesForCalibrates = nbrValuesForCalibrates;
            _schemeSelect = schemeSelect;
            _sensorDistance = sensorDistance;
            _calibrateValue = calibrateValue;
            _koeff1 = koeff1;
            _nu = nu;
        }


        public FlowConfigStruct(byte[] data)
        {
            //TODO: write all data into struct
            var str = "";
            var builder = new StringBuilder();
            foreach (var b in data)
            {
                builder.Append(b + " ");
            }
            Debug.WriteLine("[Config data]: " + builder.ToString());
        }

        public byte PipeDiametr
        {
            get { return _pipeDiametr; }
            set { _pipeDiametr = value; }
        }

        public float C0
        {
            get { return _c0; }
            set { _c0 = value; }
        }

        public float Angle
        {
            get { return _angle; }
            set { _angle = value; }
        }

        public float NullThresold
        {
            get { return _nullThresold; }
            set { _nullThresold = value; }
        }

        public byte NbrValuesForAvg
        {
            get { return _nbrValuesForAvg; }
            set { _nbrValuesForAvg = value; }
        }

        public byte NbrValuesForCalibrates
        {
            get { return _nbrValuesForCalibrates; }
            set { _nbrValuesForCalibrates = value; }
        }

        public byte SchemeSelect
        {
            get { return _schemeSelect; }
            set { _schemeSelect = value; }
        }

        public float SensorDistance
        {
            get { return _sensorDistance; }
            set { _sensorDistance = value; }
        }

        public float CalibrateValue
        {
            get { return _calibrateValue; }
            set { _calibrateValue = value; }
        }

        public float Koeff1
        {
            get { return _koeff1; }
            set { _koeff1 = value; }
        }

        public float Nu
        {
            get { return _nu; }
            set { _nu = value; }
        }

        public override string ToString()
        {
            throw  new Exception("No init");
        }
    }
}
