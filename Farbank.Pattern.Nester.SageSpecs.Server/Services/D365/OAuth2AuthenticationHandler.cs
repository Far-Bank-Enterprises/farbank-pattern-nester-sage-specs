using BlankMPS.Models;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Farbank.Pattern.Nester.SageSpecs.Server.Services.D365
{

    /// <summary>
    /// Checks for authorization headers on requests and attempts to apply 
    /// an OAuth2 token via ADAL.
    /// </summary>
    internal class OAuth2AuthenticationHandler : DelegatingHandler
    {
        /// <summary>
        /// The header to use for OAuth authentication.
        /// </summary>

        private readonly D365OAuthConfiguration _configuration;
        private AuthenticationResult _auth;

        public OAuth2AuthenticationHandler(IOptions<D365OAuthConfiguration> options)
        {
            _configuration = options.Value;
        }

        public async Task<string> GetTokenAsync()
        {
            var expired = isTokenExpired();
            if (expired)
            {

                //this overload has a token cache may want to look into that...
                if (string.IsNullOrEmpty(_configuration.ClientAppSecret))
                {
                    Console.WriteLine("Please fill AAD application secret in ClientConfiguration if you choose authentication by the application.");
                    throw new Exception("Failed OAuth by empty application secret.");
                }
                try
                {
                    // OAuth through application by application id and application secret.
                    var cred = ConfidentialClientApplicationBuilder.Create(_configuration.ClientAppId)
                        .WithClientSecret(_configuration.ClientAppSecret)
                        .WithAuthority(new Uri(_configuration.Tenant))
                        .Build();

                    var acquireTokenHandler = cred.AcquireTokenForClient(new List<string> { $"{_configuration.BaseAddress_BlankScheduler}/.default" });
                    _auth = await acquireTokenHandler.ExecuteAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("Failed to authenticate with AAD by application with exception {0} and the stack trace {1}", ex.ToString(), ex.StackTrace));
                    throw new Exception("Failed to authenticate with AAD by application.");
                }
                // Create and get JWT token
            }
            return _auth.AccessToken;
        }

        public bool isTokenExpired()
        {
            if (_auth == null)
            {
                return true;
            }
            return DateTime.Now >= _auth.ExpiresOn;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            // See if the request has an authorize header
            var auth = request.Headers.Authorization;
            if (auth != null || isTokenExpired())
            {
                var token = await GetTokenAsync();
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            //var resp = httpClient.SendAsync(request, cancellationToken);
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
