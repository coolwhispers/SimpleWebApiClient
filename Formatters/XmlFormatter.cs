using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SimpleWebApiClient.Formatters
{
    /// <summary>
    /// XML Formatter 
    /// TODO: Serializer & Deserialize
    /// </summary>
    /// <seealso cref="SimpleWebApiClient.IFormatter" />
    internal class XmlFormatter : IFormatter 
    {
        public string ContentType => "application/xml";

        public bool DataCheck(object obj)
        {
            return obj != null;
        }

        public T Deserialize<T>(string data)
        {
            var reader = XmlReader.Create(data);

            var serializer = new XmlSerializer(typeof(T));

            return (T)serializer.Deserialize(reader);
        }

        public HttpContent Serializer(object obj)
        {
            var serializer = new XmlSerializer(obj.GetType());

            var xml = string.Empty;
            using (var sw = new StringWriter())
            {
                using (var writer = XmlWriter.Create(sw))
                {
                    serializer.Serialize(writer, obj);
                    xml = sw.ToString();
                }
            }

            var content = new StringContent(xml);
            content.Headers.ContentType.MediaType = ContentType;

            return content;
        }
    }
}
