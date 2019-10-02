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
            return GetAsync<string>(apiPath).GetAwaiter().GetResult();
        }

        public T Get<T>(string apiPath)
        {
            return GetAsync<T>(apiPath).GetAwaiter().GetResult();
        }
        public async Task<string> GetAsync(string apiPath)
        {
            return await GetAsync<string>(apiPath);
        }

        public async Task<T> GetAsync<T>(string apiPath)
        {
            return await Connect<T>(HttpMethod.Get, apiPath, null);
        }

        public string Post(string apiPath, object obj)
        {
            return PostAsync<string>(apiPath, obj).GetAwaiter().GetResult();
        }

        public T Post<T>(string apiPath, object obj)
        {
            return PostAsync<T>(apiPath, obj).GetAwaiter().GetResult();
        }
        public async Task<string> PostAsync(string apiPath, object obj)
        {
            return await PostAsync<string>(apiPath, obj);
        }

        public async Task<T> PostAsync<T>(string apiPath, object obj)
        {
            return await Connect<T>(HttpMethod.Post, apiPath, obj);
        }

        public string Put(string apiPath, object obj)
        {
            return PutAsync<string>(apiPath, obj).GetAwaiter().GetResult();
        }

        public T Put<T>(string apiPath, object obj)
        {
            return PutAsync<T>(apiPath, obj).GetAwaiter().GetResult();
        }

        public async Task<string> PutAsync(string apiPath, object obj)
        {
            return await PutAsync<string>(apiPath, obj);
        }

        public async Task<T> PutAsync<T>(string apiPath, object obj)
        {
            return await Connect<T>(HttpMethod.Put, apiPath, obj);
        }

        public string Delete(string apiPath)
        {
            return DeleteAsync<string>(apiPath).GetAwaiter().GetResult();
        }

        public T Delete<T>(string apiPath)
        {
            return DeleteAsync<T>(apiPath).GetAwaiter().GetResult();
        }

        public async Task<string> DeleteAsync(string apiPath)
        {
            return await DeleteAsync<string>(apiPath);
        }

        public async Task<T> DeleteAsync<T>(string apiPath)
        {
            return await Connect<T>(HttpMethod.Delete, apiPath, null);
        }
    }
}
