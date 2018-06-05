using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebApiClient
{
    internal interface IFormFile
    {
        string Name { get; }

        string FileName { get; }

        byte[] Bytes { get; }
    }

    internal class FormFile : IFormFile
    {
        public FormFile(string name, string fileName, Stream stream)
        {
            if (stream == null)
            {
                throw new Exception("stream is null");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("name is null or emtpy");
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new Exception("fileName is null or emtpy");
            }

            Name = name;
            FileName = fileName;
            Bytes = stream.ReadToEnd();
        }

        public string Name { get; }

        public string FileName { get; }

        public byte[] Bytes { get; }
    }
}
