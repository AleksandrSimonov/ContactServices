using Contact.Attributes;
using System;
using System.Text.RegularExpressions;

namespace Contact.ContactValidation
{
    public class ContactValidater
    {
        private Contact _contact;
        private Type _type = typeof(Contact);
        public ContactValidatorResult Result { get; }
        public ContactValidater()
        {
            Result = new ContactValidatorResult();
        }

        private bool ValidateName()
        {
            var name = nameof(Contact.Name);
            var property = _type.GetProperty(name);
            var customAttribute = (MaxLengthAttribute) property.GetCustomAttributes(typeof(MaxLengthAttribute), false)[0];
            var result = customAttribute.MaxLength.CompareTo(_contact.Name.Length);

            if (result < 0)
                Result.ErrorsMessage.Add("максимальная длина " + property + " " + customAttribute.MaxLength);
            return Convert.ToBoolean(result);
        }

        private bool ValidateSurname()
        {
            var name = nameof(Contact.Surname);
            var property = _type.GetProperty(name);
            var customAttribute = (MaxLengthAttribute) property.GetCustomAttributes(typeof(MaxLengthAttribute), false)[0];
            var result = customAttribute.MaxLength.CompareTo(_contact.Surname.Length);

            if (result < 0)
                Result.ErrorsMessage.Add("максимальная длина " + property + " " + customAttribute.MaxLength);
            return Convert.ToBoolean(result);
        }

        private bool ValidateLastname()
        {
            var name = nameof(Contact.Lastname);
            var property = _type.GetProperty(name);
            var customAttribute = (MaxLengthAttribute) property.GetCustomAttributes(typeof(MaxLengthAttribute), false)[0];
            var result = customAttribute.MaxLength.CompareTo(_contact.Lastname.Length);

            if (result < 0)
                Result.ErrorsMessage.Add("максимальная длина " + property + " " + customAttribute.MaxLength);
            return Convert.ToBoolean(result);
        }

        private bool ValidateAge()
        {
            var name = nameof(Contact.Birthday);
            var property = _type.GetProperty(name);
            var minBirthdayAttribute = (MinBirthdayAttribute) property.GetCustomAttributes(typeof(MinBirthdayAttribute), false)[0];
            var birthday = new DateTime(minBirthdayAttribute.MinYear, minBirthdayAttribute.MinMonth, minBirthdayAttribute.MinDay);
            var result = _contact.Birthday.CompareTo(birthday);

            if (result < 0)
                Result.ErrorsMessage.Add("Минимальная дата рождения " + property + " " + birthday);
            return Convert.ToBoolean(result);
        }

        private bool ValidatePhoneNumber()
        {
            var name = nameof(Contact.PhoneNumber);
            var property = _type.GetProperty(name);
            var regularExpressionAttribute = (RegularExpressionAttribute) property.GetCustomAttributes(typeof(RegularExpressionAttribute), false)[0];
            var regex = new Regex(regularExpressionAttribute.RegularExpression);

            var result = regex.IsMatch(_contact.PhoneNumber);

            if (!result)
                Result.ErrorsMessage.Add("Неверный формат номера телефона");
            return Convert.ToBoolean(result);

        }

        public ContactValidatorResult Validate(Contact contact)
        {
            _contact = contact;
            ValidateName();
            ValidateSurname();
            ValidateLastname();
            ValidateAge();
            ValidatePhoneNumber();

            return Result;
        }
    }
}
