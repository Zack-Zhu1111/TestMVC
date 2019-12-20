using System;
using System.Threading.Tasks;
using NLog;
using System.Net.Http;

namespace TestMVC.Core
{
    public static class HttpHelper
    {
        private static readonly ILogger _log = LogManager.GetCurrentClassLogger();

        public static async Task<T> GetAsync<T>(string assemblyInfo, string baseUrl, string requestUri)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(baseUrl);

                    var response = await client.GetAsync(requestUri);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();
                        return responseBody.FromJson<T>();
                    }

                    _log.Error($"{assemblyInfo} failed contacting the {requestUri} of {baseUrl} ", response);

                    return default(T);
                }
                catch (Exception ex)
                {
                    _log.Error(ex, $"{assemblyInfo} failed to contact {baseUrl}: {ex.Message}");

                    return default(T);
                }
            }
        }

        public static async Task<TResult> PostAsync<T, TResult>(string assemblyInfo, string baseUrl, string requestUri, T requestObject)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(baseUrl);

                    var stringContent = new StringContent(requestObject.ToJson(), System.Text.Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(requestUri, stringContent);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();
                        return responseBody.FromJson<TResult>();
                    }

                    _log.Error($"{assemblyInfo} failed contacting the {requestUri} of {baseUrl} ", response);

                    return default(TResult);
                }
                catch (Exception ex)
                {
                    _log.Error(ex, $"{assemblyInfo} failed to contact {baseUrl}: {ex.Message}");

                    return default(TResult);
                }
            }
        }
    }
}
