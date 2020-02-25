using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using Newtonsoft.Json;

namespace PizzeriaWebApi.Repositories
{
    public class WebRepositoryBase
    {
        private IConfiguration Configuration { get; set; }
        private string Url { get; set; }
        protected ILogger Logger { get; set; }

        public WebRepositoryBase(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Initialize(string configUrlName)
        {
            Url = Configuration.GetValue<string>(configUrlName);
            if (string.IsNullOrEmpty(Url))
            {
                string message = $"Could not load url from config for config name ${configUrlName}.";
                Logger.LogError(message);
                throw new ArgumentException(message);
            }
        }

        public T Get<T>(string path)
        {
            using (var w = new WebClient())
            {
                try
                {
                    if (!path.StartsWith("/"))
                    {
                        path = $"/{path}";
                    }

                    var fullPath = $"{Url}{path}";
                    var json = w.DownloadString(fullPath);
                    return JsonConvert.DeserializeObject<T>(json);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex.Message);
                    throw;
                }
            }
        }
    }
}
