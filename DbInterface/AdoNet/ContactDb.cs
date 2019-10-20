using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Contact;

namespace DbInterface.AdoNet
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
        public Organization GetOrganization(int id)
        {
            var organization = new GetData(_ConnectionString);
            return organization.GetOrganization(id);
        }
        public Contact.Contact GetContact(string surname, string name)
        {
            var contact = new GetData(_ConnectionString);
            return contact.GetContact(surname, name);
        }

        public void InsertContact(string Name, string Surname, string Lastname, int Sex, string PhoneNumber,
                                    DateTime Birthday, string TaxId, string Post, int Job)
        {
            var sqlContact = new InsertData(_ConnectionString);
            sqlContact.InsertToContact(Name, Surname, Lastname, Sex, PhoneNumber, Birthday, TaxId, Post, Job);
        }
        public void InsertOrganization(string name, string phoneNumber)
        {
            var sqlOrganization = new InsertData(_ConnectionString);
            sqlOrganization.InsertToOrganization(name, phoneNumber);
        }
        public void UpdateContact(string Name, string Surname, string Lastname, int Sex, string PhoneNumber,
                                    DateTime Birthday, string TaxId, string Post, int Job)
        {
            var sqlContact = new UpdateData(_ConnectionString);
            sqlContact.UpdateContact(Name, Surname, Lastname, Sex, PhoneNumber, Birthday, TaxId, Post, Job);
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
