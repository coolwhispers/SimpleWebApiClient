using System;
using System.Collections;
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
        public HttpCookie Cookies { get; private set; }

        private void ConstructorCookie()
        {
            _handlerInit += InitCookie;

            Cookies = new HttpCookie(_baseUrl);
        }

        private void InitCookie(ref HttpClientHandler handler)
        {
            if (Cookies._cookieContainer == null)
            {
                Cookies.Clear();
            }

            handler.CookieContainer = Cookies._cookieContainer;
        }
    }

    public class HttpCookie : IEnumerable<Cookie>
    {
        private string _baseUrl;
        internal HttpCookie(string baseUrl)
        {
            _baseUrl = baseUrl;
            Clear();
        }

        internal CookieContainer _cookieContainer { get; set; }

        private IEnumerable<Cookie> _cookies
        {
            get
            {
                return _cookieContainer.GetCookies(new Uri(_baseUrl)).Cast<Cookie>();
            }
        }

        public string this[string name]
        {
            get
            {
                var cookie = Get(name);

                if (cookie == null)
                {
                    return null;
                }

                return cookie.Value;
            }
            set
            {
                var cookie = Get(name);

                if (cookie != null)
                {
                    cookie.Value = value;
                    return;
                }

                Add(new Cookie(name, value));
            }
        }

        private Cookie Get(string name)
        {
            return _cookies.FirstOrDefault(x => x.Name == name);
        }

        public void Add(Cookie cookie)
        {
            var baseUrl = new Uri(_baseUrl);

            Uri url;
            if (baseUrl.PathAndQuery == "/")
            {
                url = baseUrl;
            }
            else
            {
                url = new Uri(_baseUrl.Replace(baseUrl.PathAndQuery, string.Empty));
            }

            _cookieContainer.Add(url, cookie);
        }

        public void Clear()
        {
            _cookieContainer = new CookieContainer();
        }

        public IEnumerator<Cookie> GetEnumerator()
        {
            return _cookies.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
