using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebApiClient.Formatters
{
    public class StringFormatter : IFormatter
    {
        IFormatter _requestFormatter;

        public StringFormatter():this(new JsonFormatter())
        {
        }

        public StringFormatter(IFormatter requestFormatter)
        {
            _requestFormatter = requestFormatter;
        }

        public string ContentType => _requestFormatter.ContentType;

        public bool DataCheck(object obj)
        {
            return _requestFormatter.DataCheck(obj);
        }

        public T Deserialize<T>(string data)
        {
            return (T)((object)data);
        }

        public HttpContent Serializer(object obj)
        {
            return _requestFormatter.Serializer(obj);
        }
    }
}
