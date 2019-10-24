using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text.Json;
using DbInterface.AdoNet;
using System.Configuration;
namespace ContactService
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ContactService
    {
        // Чтобы использовать протокол HTTP GET, добавьте атрибут [WebGet]. (По умолчанию ResponseFormat имеет значение WebMessageFormat.Json.)
        // Чтобы создать операцию, возвращающую XML,
        //     добавьте [WebGet(ResponseFormat=WebMessageFormat.Xml)]
        //     и включите следующую строку в текст операции:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";

        private readonly string _DataSource;
        public ContactService()
        {
            _DataSource = Convert.ToString(ConfigurationManager.AppSettings["DataSource"]);
        }

        [OperationContract]
        public string InsertOrUpdateContact(int Id, string Name, string Surname, string Lastname, int Sex, string PhoneNumber,
            string Birthday, string ITN, string Post, int Job)
        {
            var contactDB = new ContactDB(_DataSource);
            try
            {
                var birthday = Convert.ToDateTime(Birthday);
                contactDB.InsertOrUpdateContact(Id, Name, Surname, Lastname, Sex, PhoneNumber, birthday, ITN, Post, Job);
                return "Контакт успешно сохранен.";
            }
            catch(Exception)
            {
              return  "Не удалось сохранить контакт!\n" +
                    "Возможно контакт с таким же ИНН уже добавлен.";
            }
            
           
        }

        [OperationContract]
        public string GetContacts(string surname, string name)
        {
            var contactDB = new ContactDB(_DataSource);
            try
            {
                var contacts =  contactDB.GetContacts(surname,name);
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
            var organizationDB = new OrganizationDB(_DataSource);
            try
            {
                var organizations = organizationDB.GetAllOrganizations();
                string json = JsonSerializer.Serialize<List<Contact.Organization>>(organizations);

                return json;
            }
            catch (Exception)
            {
                return "На сервере произошла ошибка";
            }

        }

        [OperationContract]
        public string DeleteContact(string Id)
        {
            var contactDb = new ContactDB(_DataSource);
            try
            {
                contactDb.DeleteContact(Id);
            }
            catch (Exception)
            {
                return "При удалении контакта произошла ошибка!";
            }
            return "Контакт успешно удален.";

        }
        // Добавьте здесь дополнительные операции и отметьте их атрибутом [OperationContract]
    }
}
