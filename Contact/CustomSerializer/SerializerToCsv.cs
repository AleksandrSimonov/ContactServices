using System;
using System.IO;
using System.Reflection;
using System.Text;
namespace Contact.CustomSerializer
{
    public class SerializerToCsv : ICustomSerializable
    {
        public string Extension
        {
            get
            {
                return "csv";
            }
        }
        public void Serialize(StreamWriter output, object obj, string dateFormat)
        {
            if ((output == null) || (obj == null))
                throw new ArgumentNullException();

            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            var csvName = new StringBuilder();
            var csvValue = new StringBuilder();

            // First line contains field names
            object prpValue = "";
            foreach (PropertyInfo prp in properties)
            {
                if (prp.CanRead)
                {

                    csvName.Append(prp.Name).Append(';');
                    prpValue = prp.GetValue(obj, null);

                    if (prpValue.GetType() == typeof(DateTime))
                    {
                        var dateTime = Convert.ToDateTime(prpValue);
                        csvValue.Append(dateTime.ToString(dateFormat)).Append(';');
                    }
                    else
                        csvValue.Append(prp.GetValue(obj, null)).Append(';');
                }
            }
            csvName.Length--; // Remove last ";"
            csvValue.Length--;

            //after your loop
            output.WriteLine(csvName);
            output.WriteLine(csvValue);

        }

    }
}
