using SimpleWebApiClient.Formatters;
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
        public IFormatter Formatter { get; set; }
        
        private void ConstructorFormatter()
        {
            Formatter = new JsonFormatter();
        }        
    }
}
