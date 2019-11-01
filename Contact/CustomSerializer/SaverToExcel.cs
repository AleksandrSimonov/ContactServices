using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.IO;

namespace Contact.CustomSerializer
{
    class SaverToExcel
    {
        ExcelPackage package;

        public SaverToExcel(List<Contact> contacts)
        {
            package = new ExcelPackage();
        }
        public byte[] GetFileStreamSaver(List<Contact> contacts)
        {

            FileInfo file = new FileInfo(@"C:\Users\asimonov\Documents\ContactServices\ContactService\files\contact.xlsx");
            ExcelPackage pck = new ExcelPackage(file);
                {
                    ExcelWorksheet sheet = pck.Workbook.Worksheets.Add("Contacts");
                    sheet.Cells[1, 1].Value = "Id";
                    sheet.DefaultColWidth = 15;
                    sheet.Cells[1, 2].Value = "Имя"; 
                    sheet.Cells[1, 3].Value = "Фамилия";
                    sheet.Cells[1, 4].Value = "Отчество";
                    sheet.Cells[1, 5].Value = "Пол";
                    sheet.Cells[1, 6].Value = "Номер телефона";
                    sheet.Column(6).Width = 30;
                    sheet.Cells[1, 7].Value = "День рождения";
                    sheet.Cells[1, 8].Value = "ИНН";
                    sheet.Cells[1, 9].Value = "Должность";
                    sheet.Cells[1, 10].Value = "Работа";
                    sheet.Column(7).Style.Numberformat.Format = "dd.MM.yyyy";
  
                    sheet.Cells[2, 1].LoadFromCollection(contacts);
                    pck.Save();
                    return pck.GetAsByteArray();
                }
        }
    }
}
