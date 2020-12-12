namespace DocJc.Settings
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using Contracts.Services;
    using Model.Settings;
    using Newtonsoft.Json;

    public class AppSettingsManager : IAppSettingsManager
    {
        private AppSettings _settings;

        private const string Namespace = "DocJc";
        private const string FileName = "appsettings.json";

        public AppSettings GetSettings()
        {
            if (_settings == null)
            {
                try
                {
                    var assembly = typeof(App).GetTypeInfo().Assembly;
                    var stream = assembly.GetManifestResourceStream($"{Namespace}.{FileName}");

                    using (var reader = new StreamReader(stream ?? throw new InvalidOperationException(
                        "No available app.settings file")))
                    {
                        var json = reader.ReadToEnd();
                        _settings = JsonConvert.DeserializeObject<AppSettings>(json);
                    }
                }
                catch (Exception)
                {
                    Debug.WriteLine("Unable to load secrets file");
                }
            }

            return _settings;
        }
    }
}
