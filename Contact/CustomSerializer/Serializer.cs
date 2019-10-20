using System;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;

namespace Contact.CustomSerializer
{
    public class Serializer
    {
        private OutputFormat _OutputFormat;
        private string _FileName = "contact";
        private ICustomSerializable _Serializer;
        private bool _Overwrite;
        public Serializer(OutputFormat outputFormat)
        {
            _OutputFormat = outputFormat;
        }
        public void Serialize(Contact contact, bool overwrite, string dateFormat)
        {
            _Overwrite = overwrite; 
            if (_OutputFormat == OutputFormat.Xml)
                _Serializer = new SerializerToXml();
            else if (_OutputFormat == OutputFormat.Csv)
                _Serializer = new SerializerToCsv();
            else
                throw new ArgumentOutOfRangeException();

            string fileName = GetNewFileName();
            using (var writer = new StreamWriter(fileName, false))
            {
                _Serializer.Serialize(writer, contact,dateFormat);
            }
        }
        public string GetOnlyName(FileInfo file)
        {
            var notNameReg = new Regex(@"(?<notName>\s?(\(\d+\))?\s?\..+)$");
            string notName = notNameReg.Match(file.Name).Groups["notName"].Value;
            string onlyName = file.Name.Remove(file.Name.Length - notName.Length);
            return onlyName;
        }
        private string GetNewFileName()
        {

            var file = new FileInfo(_FileName + "." + _Serializer.Extension);
            if (_Overwrite)
                return file.Name;
            if (File.Exists(Directory.GetCurrentDirectory() + @"\" + file.Name))
            {
                string onlyName = GetOnlyName(file);
                var indexNumberReg = new Regex(@"\((?<indexNumber>\d+)\)\s?\..+$");


                var files = Directory.GetFiles(Directory.GetCurrentDirectory());
                int indexNumber = 0, maxIndexNumber = 0;

                foreach (var fp in files)
                {
                    var f = new FileInfo(fp);
                    if (indexNumberReg.IsMatch(f.Name))
                    {
                        indexNumber = int.Parse(indexNumberReg.Match(f.Name).Groups["indexNumber"].Value);
                        if (indexNumber > maxIndexNumber)
                        {
                            maxIndexNumber = indexNumber;
                        }
                    }
                }

                return onlyName + $" ({indexNumber + 1}) {file.Extension}";
            }
            return file.Name;
        }
        public void Serialize(string fileName, Contact contact, bool overwrite, string dateFormat)
        {
            _FileName = fileName;
            Serialize(contact, overwrite, dateFormat);
        }
    }
}
