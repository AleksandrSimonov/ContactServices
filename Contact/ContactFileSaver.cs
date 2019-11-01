using Contact.CustomSerializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Contact
{
    public class ContactFileSaver : IDisposable
    {
        private bool _isDispose = true;
        private string _path = @"C:\Users\asimonov\Documents\ContactServices\ContactService\files\file1.csv";

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
        public FileStream FileStream { get; }
        public ContactFileSaver()
        {
            FileStream = new FileStream(_path, FileMode.Create, System.Security.AccessControl.FileSystemRights.FullControl, FileShare.Write, 4096, FileOptions.None);
            StreamWriter = new StreamWriter(FileStream, Encoding.UTF8);
            _isDispose = false;
        }
        public void Save(Contact person)
        {
            var serealizer = new SerializerToCsv();
            serealizer.Serialize(StreamWriter, person, null);
            Dispose();
        }
        public void Save(List<Contact> persons)
        {
            var serealizer = new SerializerToCsv();
            foreach(var person in persons)
            serealizer.Serialize(StreamWriter, person, null);
            Dispose();
        }
        public byte[] SaveToExcel(List<Contact> contacts)
        {
            var saver = new SaverToExcel(contacts);
          return saver.GetFileStreamSaver(contacts);
           // var streamSaver = new StreamWriter(stream);
            //streamSaver.Flush();
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
                FileStream.Dispose();
                _isDispose = true;
            }

        }
    }
}
