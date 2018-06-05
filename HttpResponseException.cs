using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebApiClient
{
    public class HttpResponseException : System.Exception
    {
        public HttpStatusCode StatusCode { get; private set; }

        public string Response { get; private set; }

        public Uri Uri { get; set; }

        public Dictionary<string, IEnumerable<string>> Headers { get; private set; }

        public HttpResponseException(Uri uri, string message, HttpStatusCode statusCode, string response, Dictionary<string, IEnumerable<string>> headers, Exception innerException)
            : base(message, innerException)
        {
            Uri = uri;
            StatusCode = statusCode;
            Response = response;
            Headers = headers;
        }

        public override string ToString()
        {
            return string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
        }
    }
}
