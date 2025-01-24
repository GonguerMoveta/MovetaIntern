using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fecher.Common
{
    public class CustomDateTime
    {
        private int day;
        private int month;
        private int year;
        private int hour;
        private int minute;
        private int second;
        private int millisecond;

        public int Day
        {
            get { return day; }
            set { day = value; }
        }

        public int Month
        {
            get { return month; }
            set { month = value; }
        }

        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        public int Hour
        {
            get { return hour; }
            set { hour = value; }
        }

        public int Minute
        {
            get { return minute; }
            set { minute = value; }
        }

        public int Second
        {
            get { return second; }
            set { second = value; }
        }

        public int Millisecond
        {
            get { return millisecond; }
            set { millisecond = value; }
        }

        public DateTime Value
        {
            get
            {
                return new DateTime(year > 0 ? year : 1899, 
                                    month > 0 ? month : 12, 
                                    day > 0 ? day : 30, 
                                    hour, minute, second, millisecond);
            }
        }

        public override string ToString()
        {
            
            return Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff");
        }

        public string ToOracleString()
        {
            
            return "TO_DATE('" + Value.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', 'YYYY-MM-DD HH24:MI:SS')";
        }
    }
}
