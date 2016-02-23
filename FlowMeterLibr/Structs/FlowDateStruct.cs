using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowMeterLibr.Structs
{
    public class FlowDateStruct
    {
        private int day;
        private int month;

        private int year;
        private int hour;
        private int minutes;
        private DateTime convertedDateTime;

        public FlowDateStruct()
        {
            convertedDateTime = new DateTime();
        }

        public FlowDateStruct(int day, int month, int year, int hour, int minutes)
        {
            this.day = day;
            this.month = month;
            this.year = year;
            this.hour = hour;
            this.minutes = minutes;
        }

        public FlowDateStruct(DateTime convertedDateTime)
        {
            this.convertedDateTime = convertedDateTime;
        }

        public FlowDateStruct(byte[] data)
        {
            int year = data[5] << 8 | data[4];
            this.year = year;
            this.month = data[3];
            this.day = data[2];
            this.hour = data[6];
            this.minutes = data[7];
       }
        public DateTime ConvertedDateTime
        {
            get { return convertedDateTime; }
            set { convertedDateTime = value;
            }
        }
        public DateTime GetDateTime()
        {
            return new DateTime(year,month,day,hour,minutes,0);
        }

        public override string ToString()
        {
            return convertedDateTime.ToString("dd.MM.yyyy hh:mm");
        }

        public int Day => day;

        public int Month => month;

        public int Year => year;

        public int Hour => hour;

        public int Minutes => minutes;
    }
}
