using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebApiClient.Formatters
{
    internal class NoContentFormatter : IFormatter
    {
        public string ContentType => null;
        
        public bool DataCheck(object obj)
        {
            return false;
        }

        public T Deserialize<T>(string data)
        {
            return default(T);
        }

        public HttpContent Serializer(object obj)
        {
            return null;
        }
    }
}
