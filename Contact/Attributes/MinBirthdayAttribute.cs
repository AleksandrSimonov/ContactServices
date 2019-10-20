using System;

namespace Contact.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MinBirthdayAttribute : Attribute
    {

        public int MinYear { get; set; }

        public int MinMonth { get; set; }

        public int MinDay { get; set; }

        public MinBirthdayAttribute(int minYear, int minMonth, int minDay)
        {
            MinYear = minYear;
            MinMonth = minMonth;
            MinDay = minDay;
        }
    }
}
