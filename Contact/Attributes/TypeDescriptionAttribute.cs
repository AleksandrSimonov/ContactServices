using System;

namespace Contact.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TypeDescriptionAttribute : Attribute
    {
        public string Discription { get; set; }

        public TypeDescriptionAttribute(string discription)
        {
            Discription = discription;
        }
    }
}
