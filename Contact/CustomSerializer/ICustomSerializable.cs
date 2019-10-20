using System;
using System.IO;
namespace Contact
{
    public interface ICustomSerializable
    {
        void Serialize(StreamWriter output, Object obj, string dateFormat);
        string Extension { get; }
    }
}
