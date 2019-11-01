    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using OfficeOpenXml;
    using System.Reflection;
using Contact;

    namespace DbInterface
    {
       public class ContactsExcelFile
        {
            private ExcelPackage _ExcelPackage;
            private string _DataSource;

            public ContactsExcelFile(ExcelPackage package, string dataSource)
            {
                _ExcelPackage = package;
            _DataSource = dataSource;
            }

            public List<Contact.Contact> GetContactsList()
            {
                List<Contact.Contact> contacts = new List<Contact.Contact>();

                var type = typeof(Contact.Contact);
                var properties = type.GetProperties(BindingFlags.GetProperty);
                var count = properties.Length;
                var sheet = _ExcelPackage.Workbook.Worksheets["Contacts"];


                for (var i = 2; true; i++)
                {
                    if (Convert.ToString(sheet.Cells[i, 1].Value) == "")
                        break;
                    int id = Convert.ToInt32(sheet.Cells[i, 1].Value);
                    var name = Convert.ToString(sheet.Cells[i, 2].Value);
                    var surname = Convert.ToString(sheet.Cells[i, 3].Value);
                    var lastname = Convert.ToString(sheet.Cells[i, 4].Value);

                    var Sexstr = Convert.ToString(sheet.Cells[i, 5].Value);
                    SexEnum sex = new SexEnum(Sexstr);
                  
                    var phoneNumber = Convert.ToString(sheet.Cells[i, 6].Value);
                    var birthday = Convert.ToDateTime((string)sheet.Cells[i, 7].Value);
                    var itn = Convert.ToString(sheet.Cells[i, 8].Value);
                    var post = Convert.ToString(sheet.Cells[i, 9].Value);

                    var organizationDB = new DbInterface.AdoNet.OrganizationDB(_DataSource);
                    var jobId = Convert.ToInt32(sheet.Cells[i, 10].Value);
                    var job = organizationDB.GetOrganization(jobId);
                    var contact = new Contact.Contact(id, name,surname,lastname,sex,phoneNumber,birthday, itn,post,job);
                    contacts.Add(contact);
                }
            return contacts;
            }
        }
    }
