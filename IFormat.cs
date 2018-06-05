using System;
using System.Net.Http;

namespace SimpleWebApiClient
{
    public interface IFormat
    {
        string ContentType { get; }

        HttpContent Serializer(object obj);

        T Deserialize<T>(string data);

        bool DataCheck(object obj);
    }
}
