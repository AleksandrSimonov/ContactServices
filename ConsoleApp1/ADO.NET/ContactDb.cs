using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Contact;
namespace ConsoleApp1.ADO.NET
{
  public  class ContactDb
    {
        private SqlConnectionStringBuilder _ConnectionString;

        public ContactDb()
        {
            _ConnectionString = new SqlConnectionStringBuilder();
            _ConnectionString.DataSource = ".";
            _ConnectionString.InitialCatalog = "Contacts";
            _ConnectionString.IntegratedSecurity = true;
            _ConnectionString.Pooling = false;
        }
        public Organization GetOrganization(string phoneNumber)
        {
            var organization = new GetData(_ConnectionString);
            return organization.GetOrganization(phoneNumber);
        }
        public Contact.Contact GetContact(string taxId)
        {
            var contact = new GetData(_ConnectionString);
            return contact.GetContact(taxId);
        }
        public void InsertContact(string Name, string Surname, string Lastname, string Sex, string PhoneNumber,
                                    DateTime Birthday, string TaxId, string Post, string Job)
        {
            var sqlContact = new InsertData(_ConnectionString);
            sqlContact.InsertToContact(Name, Surname, Lastname, Sex, PhoneNumber, Birthday, TaxId, Post, Job);
        }
        public void InsertContact(string Name, string Surname, string Lastname, string Sex, DateTime Birthday, string TaxId)
        {
            InsertContact(Name, Surname, Lastname, Sex, null, Birthday, TaxId, null, null);
        }
        public void InsertOrganization(string name, string phoneNumber)
        {
            var sqlOrganization = new InsertData(_ConnectionString);
            sqlOrganization.InsertToOrganization(name, phoneNumber);
        }
        public void UpdateContact(string column, string newValue, int id)
        {
            var sqlContact = new UpdateData(_ConnectionString);
            sqlContact.UpdateContact(column, newValue, id);
        }
        public void UpdateOrganization(string column, string newValue, int id)
        {
            var sqlOrganization = new UpdateData(_ConnectionString);
            sqlOrganization.UpdateOrganization(column, newValue, id);
        }
        public void DeleteContact(string taxId)
        {
            var sqlContact = new DeleteData(_ConnectionString);
            sqlContact.DeleteFromContact(taxId);

        }
        public void DeleteOrganization(string phoneNumber)
        {
            var sqlOrganization = new DeleteData(_ConnectionString);
            sqlOrganization.DeleteFromOrganization(phoneNumber);
        }
    }
}
