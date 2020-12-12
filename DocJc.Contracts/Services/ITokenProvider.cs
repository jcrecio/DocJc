namespace DocJc.Contracts.Services
{
    using Model.Models;

    public interface ITokenProvider
    {
        TokenResponse GetAccessToken();
    }
}
