using System;

namespace Contact.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RegularExpressionAttribute : Attribute
    {
        public string RegularExpression { get; set; }
        public RegularExpressionAttribute(string regularExpression)
        {
            RegularExpression = regularExpression;
        }
    }
}
