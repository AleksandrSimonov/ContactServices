using System;
using System.Collections.Generic;
using System.Linq;

namespace Contact.ContactValidation
{
    public class ContactValidatorResult
    {
        public bool IsCorrect
        {
            get
            {
                return !Convert.ToBoolean(ErrorsMessage.Count);
            }
        }
        public List<string> ErrorsMessage { get; }
        public ContactValidatorResult()
        {
            ErrorsMessage = new List<string>();
        }
        public override string ToString()
        {
            if (IsCorrect)
                return "Валидация прошла успешно.";
            else
            {
                string errors = "";
                foreach(var error in ErrorsMessage)
                {
                    errors += error + "\n";
                }
                return "При валидации возникла(и) ошибка(и):\n"+errors;
                    //+ErrorsMessage.Select(es => es.ToString() + "\n");
            }
        }
    }
}
