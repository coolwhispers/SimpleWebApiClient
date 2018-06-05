using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebApiClient
{
    public class WebProxy : IWebProxy
    {
        private Uri _uri;

        public WebProxy(string proxyUrl) : this(new Uri(proxyUrl), null)
        {
        }

        public WebProxy(Uri proxyUri, ICredentials credentials)
        {
            _uri = proxyUri;
            Credentials = credentials == null ? CredentialCache.DefaultCredentials : credentials;
        }

        public ICredentials Credentials { get; set; }

        public Uri GetProxy(Uri destination)
        {
            return _uri;
        }

        public bool IsBypassed(Uri host)
        {
            return false;
        }
    }
}
