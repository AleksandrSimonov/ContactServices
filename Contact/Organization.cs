using System;

namespace Contact
{
    public class Organization
    {

        public string Name { get; set; }
        public int Id { get; set; }
        public string PhoneNumber { get; set; }

        public Organization(int id, string name, string phoneNumber)
        {
            if ((name == null) || (phoneNumber == null))
                throw new NullReferenceException();
            Name = name;
            PhoneNumber = phoneNumber;
            Id = id;
        }
        private Organization() { }
        public override string ToString()
        {
            return Id.ToString();//$"Id: {Id} Name: {Name} PhoneNumber: {PhoneNumber}";
        }
    }
}
