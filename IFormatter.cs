using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebApiClient
{
    public interface IFormatter
    {
        string ContentType { get; }

        HttpContent Serializer(object obj);

        T Deserialize<T>(string data);

        bool DataCheck(object obj);
    }
}
