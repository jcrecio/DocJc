namespace DocJc.Service
{
    using Contracts.Services;
    using System;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;
    using Model.Models;
    using Newtonsoft.Json;

    public class TokenProvider : ITokenProvider
    {
        private readonly IAppSettingsManager _appSettingsManager;

        public TokenProvider(IAppSettingsManager appSettingsManager)
        {
            _appSettingsManager = appSettingsManager;
        }

        public TokenResponse GetAccessToken()
        {
            var appSettings = _appSettingsManager.GetSettings();

            byte[] secretBytes = Encoding.UTF8.GetBytes(appSettings.HealthSettings.SandboxUsers[0].Password);
            string computedHashString;

            using (HMACMD5 hmac = new HMACMD5(secretBytes))
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(appSettings.HealthSettings.HealthServiceAuthenticationEndpoint);
                byte[] computedHash = hmac.ComputeHash(dataBytes);

                computedHashString = Convert.ToBase64String(computedHash);
            }

            using (WebClient client = new WebClient())
            {
                client.Headers["Authorization"] = 
                    string.Concat("Bearer ", appSettings.HealthSettings.SandboxUsers[0].Username, ":", computedHashString);

                try
                {
                    string response = client.UploadString(appSettings.HealthSettings.HealthServiceAuthenticationEndpoint, "POST", "");

                    return JsonConvert.DeserializeObject<TokenResponse>(response);
                }
                catch (Exception)
                {
                    // Exception is in e.Message
                }

                return default;
            }
        }
    }
}
