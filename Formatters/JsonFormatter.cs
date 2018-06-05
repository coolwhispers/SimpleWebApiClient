using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;

namespace SimpleWebApiClient.Formatters
{
    public class JsonFormatter : IFormatter
    {
        public string ContentType => "application/json";

        public bool UseJsonDotNet { get; }

        private MethodInfo SerializerMethod;
        private MethodInfo DeserializeMethod;

        private bool JsonDotNetInit()
        {
            try
            {
                var jsonDotNet = Assembly.Load(new AssemblyName("Newtonsoft.Json"));
                var jsonConvertType = jsonDotNet.ExportedTypes.Where(x => x.FullName == "Newtonsoft.Json.JsonConvert").FirstOrDefault();
                var jsonConvertTypeInfo = jsonConvertType.GetTypeInfo();
                DeserializeMethod = jsonConvertTypeInfo.GetDeclaredMethods("DeserializeObject").FirstOrDefault(x => x.IsGenericMethodDefinition && x.GetParameters().Length == 1);
                SerializerMethod = jsonConvertTypeInfo.GetDeclaredMethods("SerializeObject").FirstOrDefault(x => x.GetParameters().Length == 1);

                return DeserializeMethod != null && SerializerMethod != null;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);

                return false;
            }
        }

        private string JsonDotNetSerializer(object obj)
        {
            return (string)SerializerMethod.Invoke(null, new[] { obj });
        }

        private T JsonDotNetDeserialize<T>(string json)
        {
            var method = DeserializeMethod.MakeGenericMethod(typeof(T));
            return (T)method.Invoke(null, new[] { json });
        }

        private string JsonSerializer(object obj)
        {
            var ms = new MemoryStream();

            var serializer = new DataContractJsonSerializer(obj.GetType());

            serializer.WriteObject(ms, obj);

            var reader = new StreamReader(ms);
            
            return reader.ReadToEnd();
        }

        private T JsonDeserialize<T>(string json)
        {
            var jsonBytes =Encoding.UTF8.GetBytes(json);

            var ms = new MemoryStream(jsonBytes);

            var serializer = new DataContractJsonSerializer(typeof(T));

            return (T)serializer.ReadObject(ms);
        }

        public JsonFormatter()
        {
            UseJsonDotNet = JsonDotNetInit();
        }

        public bool DataCheck(object obj)
        {
            return obj != null;
        }

        public T Deserialize<T>(string data)
        {
            return UseJsonDotNet ? JsonDotNetDeserialize<T>(data) : JsonDeserialize<T>(data);
        }

        public HttpContent Serializer(object obj)
        {
            var json = UseJsonDotNet ? JsonDotNetSerializer(obj) : JsonSerializer(obj);

            var content = new StringContent(json);

            content.Headers.ContentType.MediaType = ContentType;

            return content;
        }
    }
}
