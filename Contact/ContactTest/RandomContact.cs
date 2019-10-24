using System;

namespace Contact.ContactTest
{
    public class RandomContact
    {
        private Random _rand;
        public RandomContact()
        {
            _rand = new Random();
        }
        private string RandomName()
        {
            int length = _rand.Next(20);
            string name = "";
            for (int i = 0; i < length; i++)
                name += (char) _rand.Next(1072, 1103);
            return name;
        }
        private string RandomSurname()
        {
            return RandomName();
        }
        private string RandomLastname()
        {
            return RandomName();
        }
        private SexEnum RandomSex()
        {
            return (SexEnum) _rand.Next(0, 2);
        }
        private string RandomPhoneNumber()
        {
            string phoneNumber = "+";
            phoneNumber += _rand.Next(1, 10); //+X

            phoneNumber += " (";
            phoneNumber += _rand.Next(1, 10);
            phoneNumber += _rand.Next(1, 10);
            phoneNumber += _rand.Next(1, 10);
            phoneNumber += ") ";               //+X (XXX) 

            phoneNumber += _rand.Next(1, 10);
            phoneNumber += _rand.Next(1, 10); //+X (XXX) XX

            phoneNumber += " - ";
            phoneNumber += _rand.Next(1, 10);
            phoneNumber += _rand.Next(1, 10); //+X (XXX) XX - XX

            return phoneNumber;
        }
        private DateTime RandomBirthday()
        {
            int year = _rand.Next(1950, 2020);
            int month = _rand.Next(1, 13);
            int day = _rand.Next(1, 29);
            var birthday = new DateTime(year, month, day);
            return birthday;
        }
        private double RandomITN()
        {
            int length = 12;
            return _rand.Next(
               (int) Math.Pow(10, length),
               (int) Math.Pow(10, length + 1)
                              );
        }
        private string RandomPost()
        {
            return RandomName();
        }
        private Organization RandomJob()
        {
            string name = RandomName();
            string phoneNumber = RandomPhoneNumber();
            
            return new Organization(-1,name, phoneNumber);
        }
        public Contact NextShortContact() => new Contact(0,
            RandomName(),
            RandomSurname(),
            RandomLastname(),
            RandomSex(),
            RandomITN(),
            RandomBirthday());
        public Contact NextFullContact()
        {
            var fullContact = NextShortContact();
            fullContact.Job = RandomJob();
            fullContact.Post = RandomPost();
            fullContact.PhoneNumber = RandomPhoneNumber();
            return fullContact;
        }
    }
}
