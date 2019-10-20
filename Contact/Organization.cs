using System;

namespace Contact
{
    public class Organization
    {

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public Organization(string name, string phoneNumber)
        {
            if ((name == null) || (phoneNumber == null))
                throw new NullReferenceException();
            Name = name;
            PhoneNumber = phoneNumber;
        }
        private Organization() { }
        public override string ToString()
        {
            return "Name: " + Name + "\n" +
                "PhoneNumber: " + PhoneNumber;
        }
    }
}
