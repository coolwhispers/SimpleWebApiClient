
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using SimpleWebApiClient.Formatters;

namespace SimpleWebApiClient
{
    public partial class WebApiClient : IDisposable
    {
        private readonly string _baseUrl;
        private readonly TimeSpan _timeOut;

        public WebApiClient(string baseUrl) : this(baseUrl, 600)
        {
        }

        public WebApiClient(string baseUrl, int timeOut)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new Exception("baseUrl is null");
            }

            if (!baseUrl.EndsWith("/"))
            {
                baseUrl += "/";
            }

            if (timeOut < 0)
            {
                timeOut = 0;
            }

            _baseUrl = baseUrl;
            _timeOut = TimeSpan.FromSeconds(timeOut);

            InitPartialEvent();

            _constructor?.Invoke();

        }

        private void InitPartialEvent()
        {
            _constructor += ConstructorFormatter;
            _constructor += ConstructorHeader;
            _constructor += ConstructorWebProxy;
            _constructor += ConstructorCookie;
        }

        #region delegate & event

        private delegate void WebApiClientConstructor();

        private event WebApiClientConstructor _constructor;

        private delegate void InitHandler(ref HttpClientHandler handler);

        private event InitHandler _handlerInit;

        private delegate void EndResponse(HttpStatusCode httpStatusCode);

        private event EndResponse _endResponse;

        #endregion

        private async Task<T> Connect<T>(HttpMethod httpMethod, string apiPath, object obj)
        {
            var url = new Uri(_baseUrl + apiPath);
            var client = GetHttpClient();

            try
            {
                using (var request = new HttpRequestMessage())
                {
                    request.RequestUri = url;
                    request.Method = httpMethod;

                    #region RequestHeader

                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(Formatter.ContentType));

                    if (Headers.Count > 0)
                    {
                        foreach (var name in Headers.Names)
                        {
                            request.Headers.Add(name, Headers[name]);
                        }
                    }

                    #endregion

                    #region RequestContent

                    if (Formatter.DataCheck(obj))
                    {
                        request.Content = Formatter.Serializer(obj);
                    }

                    #endregion

                    #region Response

                    var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, CancellationToken.None).ConfigureAwait(false);
                    try
                    {
                        #region Get Response Header

                        var responseHeaders = response.Headers.ToDictionary(header => header.Key, header => header.Value);

                        foreach (var item in response.Content.Headers)
                        {
                            responseHeaders[item.Key] = item.Value;
                        }

                        #endregion

                        string responseData;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                            try
                            {
                                return typeof(T) == typeof(string) ? new StringFormatter().Deserialize<T>(responseData) : Formatter.Deserialize<T>(responseData);
                            }
                            catch (Exception exception)
                            {
                                throw new HttpResponseException(url, "Could not deserialize the response body.", response.StatusCode, responseData, responseHeaders, exception);
                            }
                        }
                        else if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
                        {
                            responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new HttpResponseException(url, "The HTTP status code of the response was not expected (" + (int)response.StatusCode + ").", response.StatusCode, responseData, responseHeaders, null);
                        }

                        _endResponse?.Invoke(response.StatusCode);
                    }
                    finally
                    {
                        if (response != null)
                        {
                            response.Dispose();
                        }
                    }

                    #endregion

                    return default(T);
                }
            }
            finally
            {

            }
        }

        private HttpClient GetHttpClient()
        {
            var handler = new HttpClientHandler();

            _handlerInit?.Invoke(ref handler);

            var client = new HttpClient(handler) { Timeout = _timeOut };
            
            return client;
        }

        public void Dispose()
        {
        }
    }

}