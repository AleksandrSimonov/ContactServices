
using System;

namespace Contact
{
    public class SexEnum
    {
        private Sex _Sex = 0;

        public Sex Sex
        {
            get
            {
                return _Sex;
            }
        }

        public SexEnum(Sex sex)
        {
            _Sex =  sex;
        }

        public SexEnum(string sex)
        {
            if (sex == "Муж.")
                _Sex = Sex.Male;
            if (sex == "Жен.")
                _Sex = Sex.Female;
        }
        public override string ToString()
        {
            if (Sex == Sex.Male)
                return "Муж.";
            if (Sex == Sex.Female)
                return "Жен.";
            return null;
        }

    }
    public enum Sex
    {
        Male,
        Female
    }

}
