using Contact.Attributes;
using System;

namespace Contact
{
    [Serializable]
    [TypeDescription("Person")]
    public sealed class Contact : ICloneable
    {

        [MaxLength(15)]
        public string Name { get;}


        [MaxLength(15)]
        public string Surname { get; }


        [MaxLength(15)]
        public string LastName { get;}

        public SexEnum Sex { get; }

        [RegularExpression(@"^\+\d \(\d{3}\) \d{3}-\d{2}-\d{2}$")]
        public string PhoneNumber { get; set; }

        private DateTime _Birthday;
        [MinBirthday(1980, 1, 1)]
        public DateTime Birthday
        {
            get;
        }

        public string TaxId { get; set; }

        public string Post { get; set; }

        public Organization Job { get; set; }

        private Contact() { }
        public Contact(string name, string surname, string lastname, SexEnum sex,
            string phoneNumber, DateTime birthday, string taxId, string post, Organization job)
        {
            if ((name == null) ||
              (surname == null) ||
              (lastname == null) ||
              (taxId == ""))
                throw new NullReferenceException();

            Name = name;
            Surname = surname;
            LastName = lastname;
            Sex = sex;
            PhoneNumber = phoneNumber;
            Birthday = birthday;
            TaxId = taxId;
            Post = post;
            Job = job;
        }

        public Contact(string name, string surname, string lastname, SexEnum sex, double taxId,
            DateTime birthday)
        {
            if ((name == null) ||
                (surname == null) ||
                (lastname == null) ||
                (taxId == 0))
                throw new NullReferenceException();
            Name = name;
            Surname = surname;
            LastName = lastname;
            Sex = sex;
            Birthday = birthday;
        }

        public override string ToString()
        {
            return Surname + " " + Name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != typeof(Contact))
                return false;
            return this.TaxId.Equals(((Contact) obj).TaxId);
        }

        public override int GetHashCode()
        {
            return this.TaxId.GetHashCode();
        }

        public object Clone()
        {
            return ((Contact) this.MemberwiseClone()).
                Job = new Organization(this?.Job?.Name,
                this?.Job?.PhoneNumber);
        }

        //public XmlSchema GetSchema()
        //{
        //    return (null);
        //}

        //public void ReadXml(XmlReader reader)
        //{

        //}

        //public void WriteXml(XmlWriter writer)
        //{
        //    writer.
        //}
    }
}
