using Contact.Attributes;
using System;

namespace Contact
{
    [Serializable]
    [TypeDescription("Person")]
    public sealed class Contact : ICloneable
    {
        public int Id { get; }

        [MaxLength(15)]
        public string Name { get;}


        [MaxLength(15)]
        public string Surname { get; }


        [MaxLength(15)]
        public string Lastname { get;}

        public SexEnum Sex { get; }

        [RegularExpression(@"^\+\d \(\d{3}\) \d{3}-\d{2}-\d{2}$")]
        public string PhoneNumber { get; set; }

        [MinBirthday(1980, 1, 1)]
        public DateTime Birthday
        {
            get;
        }

        public string ITN { get; set; }

        public string Post { get; set; }

        public Organization Job { get; set; }

        private Contact() { }
        public Contact(int id, string name, string surname, string lastname, SexEnum sex,
            string phoneNumber, DateTime birthday, string ITN, string post, Organization job)
        {
            if ((name == null) ||
              (surname == null) ||
              (ITN == null) ||
              (ITN == "")||
              (id==-1))
                throw new NullReferenceException();

            Id = id;
            Name = name;
            Surname = surname;
            Lastname = lastname;
            Sex = sex;
            PhoneNumber = phoneNumber;
            Birthday = birthday;
            this.ITN = ITN;
            Post = post;
            Job = job;
        }

        public Contact(int id, string name, string surname, string lastname, SexEnum sex, double ITN,
            DateTime birthday)
        {
            if ((name == null) ||
                (surname == null) ||
                (lastname == null) ||
                (ITN == 0)||
              (id == 0))
                throw new NullReferenceException();
            Id = id;
            Name = name;
            Surname = surname;
            Lastname = lastname;
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
            return this.ITN.Equals(((Contact) obj).ITN);
        }

        public override int GetHashCode()
        {
            return this.ITN.GetHashCode();
        }

        public object Clone()
        {
            return ((Contact) this.MemberwiseClone()).
                Job = new Organization(this.Job.Id, this?.Job?.Name,
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
