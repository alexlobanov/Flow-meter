using System;

namespace FlowMeterLibr.Structs
{
    public class FlowDateStruct
    {
        public FlowDateStruct()
        {
            ConvertedDateTime = new DateTime();
        }

        public FlowDateStruct(int day, int month, int year, int hour, int minutes)
        {
            Day = day;
            Month = month;
            Year = year;
            Hour = hour;
            Minutes = minutes;
        }

        public FlowDateStruct(DateTime convertedDateTime)
        {
            ConvertedDateTime = convertedDateTime;
        }

        public FlowDateStruct(byte[] data)
        {
            var year = data[5] << 8 | data[4];
            Year = year;
            Month = data[3];
            Day = data[2];
            Hour = data[6];
            Minutes = data[7];
        }

        public DateTime ConvertedDateTime { get; set; }

        public int Day { get; }

        public int Month { get; }

        public int Year { get; }

        public int Hour { get; }

        public int Minutes { get; }

        public DateTime GetDateTime()
        {
            return new DateTime(Year, Month, Day, Hour, Minutes, 0);
        }

        public override string ToString()
        {
            return ConvertedDateTime.ToString("dd.MM.yyyy hh:mm");
        }
    }
}