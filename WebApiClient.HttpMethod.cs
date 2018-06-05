using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebApiClient
{
    public partial class WebApiClient
    {
        public string Get(string apiPath)
        {
            return Get<string>(apiPath);
        }

        public T Get<T>(string apiPath)
        {
            return GetAsync<T>(apiPath).Result;
        }

        public async Task<T> GetAsync<T>(string apiPath)
        {
            return await Connect<T>(HttpMethod.Get, apiPath, null);
        }

        public string Post(string apiPath, object obj)
        {
            return PostAsync<string>(apiPath, obj).Result;
        }

        public T Post<T>(string apiPath, object obj)
        {
            return PostAsync<T>(apiPath, obj).Result;
        }

        public async Task<T> PostAsync<T>(string apiPath, object obj)
        {
            return await Connect<T>(HttpMethod.Post, apiPath, obj);
        }
        public string Put(string apiPath, object obj)
        {
            return PutAsync<string>(apiPath, obj).Result;
        }

        public T Put<T>(string apiPath, object obj)
        {
            return PutAsync<T>(apiPath, obj).Result;
        }

        public async Task<T> PutAsync<T>(string apiPath, object obj)
        {
            return await Connect<T>(HttpMethod.Put, apiPath, obj);
        }

        public string Delete(string apiPath)
        {
            return DeleteAsync<string>(apiPath).Result;
        }

        public T Delete<T>(string apiPath)
        {
            return DeleteAsync<T>(apiPath).Result;
        }

        public async Task<T> DeleteAsync<T>(string apiPath)
        {
            return await Connect<T>(HttpMethod.Delete, apiPath, null);
        }
    }
}
