using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.DAL.Base
{
    public interface IWebRepository : IDisposable
    {
        /// <summary>
        /// API位址
        /// </summary>
        string APIHost { get; }
    }

    internal class WebRepository : IWebRepository
    {
        public string APIHost { get; private set; }
        protected readonly string JSON = "application/json";
        protected WebRepository(string APIHost)
        {
            if (string.IsNullOrEmpty(APIHost))
            {
                throw new ArgumentNullException(nameof(APIHost));
            }

            this.APIHost = APIHost.Trim();
        }

        protected virtual void Dispose(bool Disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private async Task<string> GetHTTP(string Url, double Timeout)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(Url, UriKind.Absolute);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JSON));
            client.Timeout = TimeSpan.FromSeconds(Timeout);

            var response = await client.GetAsync(string.Empty);
            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return result;
            }
            else
            {
                throw new HttpRequestException($"HTTP failed, result = {result ?? string.Empty}");
            }
        }

        private async Task<TResult> GetHTTP<TResult>(string Url, double Timeout)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(Url, UriKind.Absolute);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JSON));
            client.Timeout = TimeSpan.FromSeconds(Timeout);

            var response = await client.GetAsync(string.Empty);
            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TResult>(result);
            }
            else
            {
                var errorContent = JsonConvert.DeserializeAnonymousType(result, new { Message = string.Empty });
                throw new HttpRequestException(errorContent.Message);
            }
        }

        private async Task<TResult> PostHTTP<TResult>(string Url, object Payload, double Timeout)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(Url, UriKind.Absolute);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JSON));
            client.Timeout = TimeSpan.FromSeconds(Timeout);

            using var content = new StringContent(JsonConvert.SerializeObject(Payload), Encoding.UTF8, JSON);

            var response = await client.PostAsync(string.Empty, content);
            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TResult>(result);
            }
            else
            {
                var errorContent = JsonConvert.DeserializeAnonymousType(result, new { Message = string.Empty });
                throw new HttpRequestException(errorContent.Message);
            }
        }

        /// <summary>
        /// GET REST API
        /// </summary>
        /// <param name="Path">路徑</param>
        /// <param name="Timeout">逾時秒數</param>
        /// <exception cref="HttpRequestException"></exception>
        /// <returns></returns>
        protected virtual async Task<string> Get(string Path, double Timeout = 20.0)
        {
            return await GetHTTP(string.Format("{0}/{1}", APIHost, Path), Timeout);
        }

        /// <summary>
        /// GET REST API
        /// </summary>
        /// <typeparam name="TResult">API結果</typeparam>
        /// <param name="Path">路徑</param>
        /// <param name="Timeout">逾時秒數</param>
        /// <exception cref="HttpRequestException"></exception>
        /// <returns></returns>
        protected virtual async Task<TResult> Get<TResult>(string Path, double Timeout = 20.0)
        {
            return await GetHTTP<TResult>(string.Format("{0}/{1}", APIHost, Path), Timeout);
        }

        /// <summary>
        /// POST REST API
        /// </summary>
        /// <typeparam name="TResult">API結果</typeparam>
        /// <param name="Path">路徑</param>
        /// <param name="Payload">資料物件</param>
        /// <param name="Timeout">逾時秒數</param>
        /// <exception cref="HttpRequestException"></exception>
        /// <returns></returns>
        protected virtual async Task<TResult> Post<TResult>(string Path, object Payload, double Timeout = 20.0)
        {
            return await PostHTTP<TResult>(string.Format("{0}/{1}", APIHost, Path), Payload, Timeout);
        }
    }
}
