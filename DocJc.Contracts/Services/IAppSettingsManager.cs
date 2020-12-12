namespace DocJc.Contracts.Services
{
    using Model.Settings;

    public interface IAppSettingsManager
    {
        AppSettings GetSettings();
    }
}
