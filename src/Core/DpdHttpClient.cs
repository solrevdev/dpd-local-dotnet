using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Solrevdev.DpdLocalDotnet.Core.Extensions;

namespace Solrevdev.DpdLocalDotnet.Core
{
    /// <summary>
    /// A wrapper around <see also="IHttpClientFactory" /> that given a valid set of <see also="DpdCredentials" />
    /// will make HTTP GET and HTTP POST requests to the DPD Local API
    /// </summary>
    public class DpdHttpClient
    {
        private readonly DpdCredentials _appSettings;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<DpdHttpClient> _logger;

        public DpdHttpClient(IOptions<DpdCredentials> appSettings, IHttpClientFactory clientFactory, ILogger<DpdHttpClient> logger)
        {
            _appSettings = appSettings.Value;
            _clientFactory = clientFactory;
            _logger = logger;
        }

        /// <summary>
        /// Makes a HTTP GET request to DPD, pass in the URL to call and the T type to return and it will use the
        /// <see also="System.Text.Json.JsonSerializer" /> to Deserialize back as T
        /// </summary>
        /// <param name="url">The url to call</param>
        /// <typeparam name="T">The T type to return</typeparam>
        /// <returns>Either T or null if an error is caught</returns>
        public async Task<T> GetJsonAsync<T>(string url)
        {
            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("User-Agent", _appSettings.Name);
                var client = _clientFactory.CreateClient();

                using var response = await client.SendAsync(request).ConfigureAwait(false);
                await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);

                var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    ReadCommentHandling = JsonCommentHandling.Skip,
                    AllowTrailingCommas = true
                };
                return JsonSerializer.Deserialize<T>(responseString, options);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unknown exception calling [{me}] with url [{url}] with message [{message}] and exception [{ex}] and stack [{stack}]", nameof(GetJsonAsync), url, ex.Message, ex, ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        ///Makes a HTTP POST call to DPD, pass in the URL to call and the T type to return and it will use the
        /// <see also="System.Text.Json.JsonSerializer" /> to Deserialize back as T
        /// </summary>
        /// <param name="url">The url to post to</param>
        /// <param name="parameters">The key/value pairs to post</param>
        /// <typeparam name="T">The T type to return</typeparam>
        /// <returns>Either T or null if an error is caught</returns>
        public async Task<T> PostJsonAsync<T>(string url, Dictionary<string, string> parameters)
        {
            try
            {
                var encodedContent = new FormUrlEncodedContent(parameters);
                var client = _clientFactory.CreateClient();

                using var response = await client.PostAsync(url, encodedContent).ConfigureAwait(false);
                await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);

                var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    ReadCommentHandling = JsonCommentHandling.Skip,
                    AllowTrailingCommas = true
                };
                return JsonSerializer.Deserialize<T>(responseString, options);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unknown exception calling [{me}] with url [{url}] with message [{message}] and exception [{ex}] and stack [{stack}]", nameof(PostJsonAsync), url, ex.Message, ex, ex.StackTrace);
                throw;
            }
        }
    }
}
