using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebApiClient
{
    public partial class WebApiClient
    {
        public HttpHeader Headers { get; private set; }

        private void ConstructorHeader()
        {
            Headers = new HttpHeader();
        }
    }

    public class HttpHeader : IEnumerable<KeyValuePair<string, string>>
    {
        internal HttpHeader()
        {
            headers = new Dictionary<string, string>();
        }

        private Dictionary<string, string> headers;

        /// <summary>
        /// 取得或設定HTTP Header
        /// </summary>
        /// <param name="name">Header Name</param>
        /// <returns></returns>
        public string this[string name]
        {
            get
            {
                if (headers.ContainsKey(name))
                {
                    return headers[name];
                }

                return null;
            }
            set
            {
                if (headers.ContainsKey(name))
                {
                    headers[name] = value;
                }
                else
                {
                    headers.Add(name, value);
                }
            }
        }

        /// <summary>
        /// 加入HTTP Header
        /// </summary>
        /// <param name="name">Header Name</param>
        /// <param name="value">Value</param>
        public void Add(string name, string value)
        {
            this[name] = value;
        }

        /// <summary>
        /// 移除HTTP Header
        /// </summary>
        /// <param name="name">Header Name</param>
        public void Remove(string name)
        {
            if (headers.ContainsKey(name))
            {
                headers.Remove(name);
            }
        }

        public void AddToken(string token)
        {
            Add("Authorization", "Bearer " + token);
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return headers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return headers.GetEnumerator();
        }

        public IEnumerable<string> Names => headers.Keys;

        public int Count => headers.Count;

    }
}
