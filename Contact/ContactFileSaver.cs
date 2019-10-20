using Contact.CustomSerializer;
using System;
using System.IO;
using System.Text;

namespace Contact
{
    public class ContactFileSaver : IDisposable
    {
        private bool _isDispose = true;
        private string _path = "file.csv";

        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }
        public StreamWriter StreamWriter { get; }

        public ContactFileSaver()
        {
            StreamWriter = new StreamWriter(_path, false, Encoding.UTF8);
            _isDispose = false;
        }
        public void Save(Contact person)
        {
            var serealizer = new SerializerToCsv();
            serealizer.Serialize(StreamWriter, person, null);
            Dispose();
        }
        public void Save(Contact person, string path)
        {
            _path = path;
            Save(person);
        }
        public void Dispose()
        {
            if (!_isDispose)
            {
                StreamWriter.Dispose();
                _isDispose = true;
            }

        }
    }
}
