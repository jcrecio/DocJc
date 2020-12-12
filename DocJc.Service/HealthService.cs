namespace DocJc.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Contracts.Services;
    using Model.Models;
    using Newtonsoft.Json;
    using Utils;

    public class HealthService : IHealthService
    {
        private readonly IAppSettingsManager _appSettingsManager;
        private readonly ITokenProvider _tokenProvider;

        public HealthService(
            IAppSettingsManager appSettingsManager, 
            ITokenProvider tokenProvider)
        {
            _appSettingsManager = appSettingsManager;
            _tokenProvider = tokenProvider;
        }

        public Task<IList<BaseEntity>> GetBodyLocations()
        {
            throw new NotImplementedException();
        }

        public Task<IList<BaseEntity>> GetIssues()
        {
            throw new NotImplementedException();
        }

        public async Task<IList<BaseEntity>> GetSymptoms(string keyword = null)
        {
            var token = _tokenProvider.GetAccessToken().Token;

            var httpClient = new HttpClient();
            IList<BaseEntity> response = null;

            try
            {
                var symptoms = await httpClient.GetStringAsync(
                    $"{_appSettingsManager.GetSettings().HealthSettings.HealthServiceDataEndpoint}/symptoms?token={token}&language=es-es");

                response = JsonConvert.DeserializeObject<IList<BaseEntity>>(symptoms);
            }
            catch (Exception ex)
            {
                // ignore
            }


            if (keyword != null)
            {
                response = response?
                    .Where(r => r.Name.ContainsCaseInsensitive(keyword))
                    .ToList();
            }

            return response;
        }

        public async Task<IList<Diagnostic>> GetDiagnostics(IEnumerable<BaseEntity> symptoms)
        {
            var token = _tokenProvider.GetAccessToken().Token;

            var httpClient = new HttpClient();
            IList<Diagnostic> response = null;

            try
            {
                var diagnostics = await httpClient.GetStringAsync(
                    $"{_appSettingsManager.GetSettings().HealthSettings.HealthServiceDataEndpoint}/diagnosis?"
                    + $"symptoms=[{string.Join(",",symptoms.Select(s => s.ID))}]"
                    + $"&token={token}&language=es-es&gender=male&year_of_birth=1982");

                response = JsonConvert.DeserializeObject<IList<Diagnostic>>(diagnostics);
            }
            catch (Exception ex)
            {
                // ignore
            }

            return response;
        }
    }
}
