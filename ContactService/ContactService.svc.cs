using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text.Json;
using DbInterface.AdoNet;
using System.Configuration;
using System.IO;
using System.Web;
using System.Net.Http;
using System.Net;
using System.ServiceModel.Web;
using OfficeOpenXml;
using DbInterface;

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
            Logger.InitLogger();
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
        public MemoryStream GetContactsFile(string surname, string name)
        {
            var contactDB = new ContactDB(_DataSource);
            Contact.ContactFileSaver fileSaver=null;
   
            try
            {

                var contacts = contactDB.GetContacts(surname, name);
                fileSaver = new Contact.ContactFileSaver();
          
                var stream =  new MemoryStream(fileSaver.SaveToExcel(contacts));
                Logger.Log.Info("поток отправлен пользователю");

                //WebOperationContext.Current.OutgoingResponse.ContentType ="application/vnd.ms-excel";

                return stream;

                //return null;// "http://localhost:8091/files/file.csv";

            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                return null;
            }
            finally
            {
                if (fileSaver != null)
                    fileSaver.Dispose();
              
            }
        }

        [OperationContract]
        public string UploadContact(byte[] Contacts)
        {
            try
            {
                var pack = new ExcelPackage(new MemoryStream(Contacts));
                ContactsExcelFile excelFile = new ContactsExcelFile(pack, _DataSource);
                var contacts = excelFile.GetContactsList();
                var db = new ContactDB(_DataSource);
                var result = db.InsertOrUpdateContact(contacts);

                if (result)
                {
                    return "Контакты успешно обработаны сервером";
                }
                else
                {
                    return "В ходе добавления контактов возникла ошика!\n" +
                        "Один или несколько контактов не были обноавлены или добавлены";
                }
                
                
            }
            catch(Exception e)
            {
                return "В ходе добавления контактов возникла ошика!\n" +
                                        "Один или несколько контактов не были обноавлены или добавлены";
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
