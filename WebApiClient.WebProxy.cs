using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebApiClient
{
    public partial class WebApiClient
    {
        public IWebProxy WebProxy { get; set; }

        private void ConstructorWebProxy()
        {
            _handlerInit += InitWebProxy;
        }

        private void InitWebProxy(ref HttpClientHandler handler)
        {
            if (WebProxy != null)
            {
                handler.Proxy = WebProxy;
                handler.UseProxy = true;
            }
        }
    }
}
