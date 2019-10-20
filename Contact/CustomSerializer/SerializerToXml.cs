using System;
using System.IO;
using System.Xml.Serialization;

namespace Contact.CustomSerializer
{
    public class SerializerToXml : ICustomSerializable
    {
        public string Extension
        {
            get
            {
                return "xml";
            }
        }
        public void Serialize(StreamWriter output, object obj, string dateFormat)
        {
            if ((output == null) || (obj == null))
                throw new ArgumentNullException();

            XmlSerializer formatter = new XmlSerializer(obj.GetType());
            formatter.Serialize(output, obj);

        }
    }
}
