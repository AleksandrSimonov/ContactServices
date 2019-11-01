using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.Reflection;

namespace Contact
{
    class ContactsExcelFile
    {
        private ExcelPackage _ExcelPackage;
        public ContactsExcelFile(ExcelPackage package)
        {
            _ExcelPackage = package;
        }

        public void GetContactsList()
        { List<Contact> contacts = new List<Contact>();

            var type = typeof(Contact);
            var properties = type.GetProperties(BindingFlags.GetProperty);
            var count = properties.Length;
            var sheet = _ExcelPackage.Workbook.Worksheets["Contacts"];
            

            for(var i=2; true; i++)
            {
                if (Convert.ToString(sheet.Cells[i, 1].Value) == "")
                    break;
              int id = Convert.ToInt32(sheet.Cells[i, 1].Value);
              var name = Convert.ToString(sheet.Cells[i, 2].Value);
              var surname = Convert.ToString(sheet.Cells[i, 3].Value);
              var lastname = Convert.ToString(sheet.Cells[i, 4].Value);
              var sex = (SexEnum) sheet.Cells[i, 5].Value;
              var phoneNumber = Convert.ToString(sheet.Cells[i, 6].Value);
              var birthday = Convert.ToDateTime(sheet.Cells[i, 7].Value);
              var itn = Convert.ToString(sheet.Cells[i, 8].Value);
              var post = Convert.ToString(sheet.Cells[i, 9].Value);
              var job= Convert.ToUInt32(sheet.Cells[i, 10].Value);
               
            } 
        }
    }
}
