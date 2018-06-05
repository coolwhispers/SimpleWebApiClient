using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebApiClient.Formatters
{
    /// <summary>
    /// File Formatter
    /// TODO: Serializer & Deserialize
    /// </summary>
    /// <seealso cref="SimpleWebApiClient.IFormatter" />
    internal class FileFormatter : IFormatter
    {
        private IFormatter _responseFormatter;

        public FileFormatter() : this(new NoContentFormatter())
        {
        }

        public FileFormatter(IFormatter responseFormatter)
        {
            _responseFormatter = responseFormatter;
            _uploadFiles = new List<IFormFile>();
        }

        public string ContentType => _responseFormatter.ContentType;

        public bool DataCheck(object obj)
        {
            return _uploadFiles.Count > 0;
        }

        public T Deserialize<T>(string data)
        {
            return _responseFormatter.Deserialize<T>(data);
        }

        public HttpContent Serializer(object obj)
        {
            var multiPartContent = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(System.Globalization.CultureInfo.InvariantCulture));
            foreach (var file in _uploadFiles)
            {
                var content = new ByteArrayContent(file.Bytes);
                content.Headers.Add("Content-Type", "application/octet-stream");
                multiPartContent.Add(content, file.Name, file.FileName);
            }

            return multiPartContent;
        }

        public void Add(IFormFile file)
        {
            _uploadFiles.Add(file);
        }


        private IList<IFormFile> _uploadFiles;
    }
}
