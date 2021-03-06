﻿using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Develappers.BillomatNet.Api.Net
{
    public class HttpClient
    {
        private const string HeaderNameApiKey = "X-BillomatApiKey";
        private const string HeaderNameAppId = "X-AppId";
        private const string HeaderNameAppSecret = "X-AppSecret";

        public HttpClient(string billomatId, string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("invalid api key", nameof(apiKey));
            }

            if (string.IsNullOrWhiteSpace(billomatId))
            {
                throw new ArgumentException("invalid billomat id", nameof(billomatId));
            }

            BillomatId = billomatId;
            ApiKey = apiKey;
        }

        public string ApiKey { get; private set; }

        public string BillomatId { get; private set; }

        public string AppId { get; set; }

        public string AppSecret { get; set; }

        public async Task<byte[]> GetBytesAsync(Uri relativeUri, CancellationToken token = default(CancellationToken))
        {
            var baseUri = new Uri($"https://{BillomatId}.billomat.net/");
            var builder = new UriBuilder(new Uri(baseUri, relativeUri));
            var uri = builder.ToString();

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Method = "GET";
            //httpWebRequest.Accept = "application/json";
            httpWebRequest.Headers.Add(HeaderNameApiKey, ApiKey);

            if (!string.IsNullOrWhiteSpace(AppId))
            {
                httpWebRequest.Headers.Add(HeaderNameAppId, AppId);
            }
            if (!string.IsNullOrWhiteSpace(AppSecret))
            {
                httpWebRequest.Headers.Add(HeaderNameAppSecret, AppSecret);
            }


            var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
            var responseStream = httpResponse.GetResponseStream();
            if (responseStream == null)
            {
                throw new IOException("response stream was null!");
            }
           
            var ms = new MemoryStream();
            responseStream.CopyTo(ms);
            return ms.ToArray();
        }

        public Task<string> GetAsync(Uri relativeUri, CancellationToken token = default(CancellationToken))
        {
            return GetAsync(relativeUri, null, token);
        }

        public async Task<string> GetAsync(Uri relativeUri, string query, CancellationToken token = default(CancellationToken))
        {
            var baseUri = new Uri($"https://{BillomatId}.billomat.net/");
            var builder = new UriBuilder(new Uri(baseUri, relativeUri));
            if (!string.IsNullOrEmpty(query))
            {
                builder.Query = query;
            }
            var uri = builder.ToString();

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Method = "GET";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.Headers.Add(HeaderNameApiKey, ApiKey);

            if (!string.IsNullOrWhiteSpace(AppId))
            {
                httpWebRequest.Headers.Add(HeaderNameAppId, AppId);
            }
            if (!string.IsNullOrWhiteSpace(AppSecret))
            {
                httpWebRequest.Headers.Add(HeaderNameAppSecret, AppSecret);
            }


            var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
            var responseStream = httpResponse.GetResponseStream();
            if (responseStream == null)
            {
                throw new IOException("response stream was null!");
            }

            string result;
            using (var streamReader = new StreamReader(responseStream))
            {
                result = await streamReader.ReadToEndAsync();
            }

            return result;
        }

        public async Task<string> DeleteAsync(Uri relativeUri, CancellationToken token)
        {
            var baseUri = new Uri($"https://{BillomatId}.billomat.net/");
            var builder = new UriBuilder(new Uri(baseUri, relativeUri));
            var uri = builder.ToString();

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Method = "DELETE";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.Headers.Add(HeaderNameApiKey, ApiKey);

            if (!string.IsNullOrWhiteSpace(AppId))
            {
                httpWebRequest.Headers.Add(HeaderNameAppId, AppId);
            }
            if (!string.IsNullOrWhiteSpace(AppSecret))
            {
                httpWebRequest.Headers.Add(HeaderNameAppSecret, AppSecret);
            }


            var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
            var responseStream = httpResponse.GetResponseStream();
            if (responseStream == null)
            {
                throw new IOException("response stream was null!");
            }

            string result;
            using (var streamReader = new StreamReader(responseStream))
            {
                result = await streamReader.ReadToEndAsync();
            }

            return result;
        }

        public async Task<string> PutAsync(Uri relativeUri, string data, CancellationToken token)
        {
            var baseUri = new Uri($"https://{BillomatId}.billomat.net/");
            var builder = new UriBuilder(new Uri(baseUri, relativeUri));
            var uri = builder.ToString();

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Method = "PUT";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add(HeaderNameApiKey, ApiKey);

            if (!string.IsNullOrWhiteSpace(AppId))
            {
                httpWebRequest.Headers.Add(HeaderNameAppId, AppId);
            }
            if (!string.IsNullOrWhiteSpace(AppSecret))
            {
                httpWebRequest.Headers.Add(HeaderNameAppSecret, AppSecret);
            }

            var reqStream = httpWebRequest.GetRequestStream();
            var bytes = Encoding.UTF8.GetBytes(data);
            await reqStream.WriteAsync(bytes, 0, bytes.Length, token).ConfigureAwait(false);
            reqStream.Close();

            var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
            var responseStream = httpResponse.GetResponseStream();
            if (responseStream == null)
            {
                throw new IOException("response stream was null!");
            }

            string result;
            using (var streamReader = new StreamReader(responseStream))
            {
                result = await streamReader.ReadToEndAsync();
            }

            return result;
        }
    }
}
