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
    /// The main entry point for talking to the DPD Local API
    /// </summary>
    public class DpdLocalApiClient
    {
        private readonly DpdCredentials _appSettings;
        private readonly ILogger<DpdLocalApiClient> _logger;
        private readonly DpdHttpClient _client;

        /// <summary>
        /// Default constructor that takes in the dependencies that it needs to get configuration, logging and external http client calls
        /// </summary>
        /// <param name="appSettings">An instance of <see="DpdCredentials" /></param>
        /// <param name="logger">An ILogger for this class</param>
        /// <param name="client">A <see="DpdHttpClient" /> which wraps http calls </param>
        public DpdLocalApiClient(IOptions<DpdCredentials> appSettings, ILogger<DpdLocalApiClient> logger, DpdHttpClient client)
        {
            _appSettings = appSettings.Value;
            _logger = logger;
            _client = client;
        }

        public void Login()
        {
            _logger.LogInformation("Logging in to DPD Local API");
            AssertDpdSettings();
        }

        public async Task LoginAsync()
        {
            _logger.LogInformation("Logging in to DPD Local API - Async");
            AssertDpdSettings();
            await Task.CompletedTask.ConfigureAwait(false);
        }

        public void InsertShipment()
        {
            _logger.LogInformation("Inserting shipment");
            AssertDpdSettings();
        }

        public void GetLabels()
        {
            _logger.LogInformation("Getting labels");
            AssertDpdSettings();
        }

        /// <summary>
        /// Checks that <see="DpdCredentials" /> have been set by appsettings.json or similar.
        /// </summary>
        private void AssertDpdSettings()
        {
            const string missingSettingTemplate = "The {0} is either null or empty please check the {1} section in your appsettings.json";

            if (_appSettings == null)
            {
                throw new ArgumentNullException(nameof(DpdCredentials), $"The {nameof(DpdCredentials)} are null please check your appsettings.json file");
            }

            if (string.IsNullOrWhiteSpace(_appSettings.Name))
            {
                throw new ArgumentNullException(nameof(_appSettings.Name), string.Format(missingSettingTemplate, nameof(_appSettings.Name), nameof(DpdCredentials)));
            }

            if (string.IsNullOrWhiteSpace(_appSettings.ApiUrl))
            {
                throw new ArgumentNullException(nameof(_appSettings.ApiUrl), string.Format(missingSettingTemplate, nameof(_appSettings.ApiUrl), nameof(DpdCredentials)));
            }

            if (string.IsNullOrWhiteSpace(_appSettings.Username))
            {
                throw new ArgumentNullException(nameof(_appSettings.Username), string.Format(missingSettingTemplate, nameof(_appSettings.Username), nameof(DpdCredentials)));
            }

            if (string.IsNullOrWhiteSpace(_appSettings.Password))
            {
                throw new ArgumentNullException(nameof(_appSettings.Password), string.Format(missingSettingTemplate, nameof(_appSettings.Password), nameof(DpdCredentials)));
            }

            if (string.IsNullOrWhiteSpace(_appSettings.AccountNumber))
            {
                throw new ArgumentNullException(nameof(_appSettings.AccountNumber), string.Format(missingSettingTemplate, nameof(_appSettings.AccountNumber), nameof(DpdCredentials)));
            }
        }
    }
}
