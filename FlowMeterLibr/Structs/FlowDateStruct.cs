using System;
using FlowMeterLibr.Сommunication;

namespace FlowMeterLibr.Structs
{
    public struct DateStruct
    {
        public byte Day { get; set; }

        public byte Month { get; set; }

        public ushort Year { get; set; }

        public byte Hour { get; set; }

        public byte Minutes { get; set; }
    }

    public class FlowDateStruct
    {
        private DateStruct _flowStruct;
        private DateTime convertedDateTime;

        public DateStruct FlowStruct
        {
            get { return _flowStruct; }
        }

        public DateTime ConvertedDateTime
        {
            get { return convertedDateTime; }
            set { convertedDateTime = value; }
        }

        public FlowDateStruct()
        {
            ConvertedDateTime = new DateTime();
        }

        public FlowDateStruct(byte[] data)
        {
            _flowStruct = data.ToStruct<DateStruct>();
            ConvertedDateTime = new DateTime((int)_flowStruct.Year, (int)_flowStruct.Month, (int)_flowStruct.Day, (int)_flowStruct.Hour, (int)_flowStruct.Minutes, 0);
        }

        public FlowDateStruct(DateStruct data)
        {
            ConvertedDateTime = new DateTime((int)data.Year, (int)data.Month, (int)data.Day, (int)data.Hour, (int)data.Minutes,0);
            _flowStruct = data;
        }

        public FlowDateStruct(DateTime convertedDateTime)
        {
            ConvertedDateTime = convertedDateTime;
            _flowStruct.Year = ushort.Parse(convertedDateTime.Year.ToString());
            _flowStruct.Month = byte.Parse(convertedDateTime.Month.ToString());
            _flowStruct.Day = byte.Parse(convertedDateTime.Day.ToString());
            _flowStruct.Hour = byte.Parse(convertedDateTime.Hour.ToString());
            _flowStruct.Minutes = byte.Parse(convertedDateTime.Minute.ToString());

        }
     

        public override string ToString()
        {
            return ConvertedDateTime.ToString("dd.MM.yyyy hh:mm");
        }
    }
}