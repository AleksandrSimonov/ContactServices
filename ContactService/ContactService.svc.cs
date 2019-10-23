using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Text.Json;
using Contact;
using DbInterface.AdoNet;

namespace WcfService2
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service2
    {
        // Чтобы использовать протокол HTTP GET, добавьте атрибут [WebGet]. (По умолчанию ResponseFormat имеет значение WebMessageFormat.Json.)
        // Чтобы создать операцию, возвращающую XML,
        //     добавьте [WebGet(ResponseFormat=WebMessageFormat.Xml)]
        //     и включите следующую строку в текст операции:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]
        public string InsertContact(string Name, string Surname, string Lastname, int Sex, string PhoneNumber,
            string Birthday, string TaxId, string Post, int Job)
        {
            ContactDb contactDb = new ContactDb();
            try
            {
                var birthday = Convert.ToDateTime(Birthday);
                contactDb.InsertContact(Name, Surname, Lastname, Sex, PhoneNumber, birthday, TaxId, Post, Job);
            }catch(Exception ex)
            {
              return  "Не удалось сохранить контакт!\n" +
                    "Возможно контакт с таким же ИНН уже добавлен.";
            }
            return "Контакт успешно сохранен.";
           
        }

        [OperationContract]
        public string GetContacts(string surname, string name)
        {
            ContactDb contactDb = new ContactDb();
            try
            {
                var contacts =  contactDb.GetContact(surname,name);
                string json = JsonSerializer.Serialize<List<Contact.Contact>>(contacts);

                return json;
            }
            catch (Exception ex)
            {
                return "На сервере произошла ошибка";
            }


        }

        [OperationContract]
        public string GetAllOrganizations()
        {
            ContactDb contactDb = new ContactDb();
            try
            {
                var organizations = contactDb.GetAllOrganizations();
                string json = JsonSerializer.Serialize<List<Contact.Organization>>(organizations);

                return json;
            }
            catch (Exception ex)
            {
                return "На сервере произошла ошибка";
            }

        }

        [OperationContract]
        public string UpdateContact(string Name, string Surname, string Lastname, int Sex, string PhoneNumber,
            string Birthday, string TaxId, string Post, int Job)
        {
            ContactDb contactDb = new ContactDb();
            try
            {
                var birthday = Convert.ToDateTime(Birthday);
                contactDb.UpdateContact(Name, Surname, Lastname, Sex, PhoneNumber, birthday, TaxId, Post, Job);
            }
            catch (Exception ex)
            {
                return "Не удалось обновить контакт!";
            }
            return "Контакт успешно обновлен.";

        }

        [OperationContract]
        public string DeleteContact(string taxId)
        {
            ContactDb contactDb = new ContactDb();
            try
            {
                contactDb.DeleteContact(taxId);
            }
            catch (Exception ex)
            {
                return "При удалении контакта произошла ошибка!";
            }
            return "Контакт успешно удален.";

        }
        // Добавьте здесь дополнительные операции и отметьте их атрибутом [OperationContract]
    }
}
