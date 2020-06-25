using BigBurguer.Api.Views;

namespace BigBurguer.Api.Services
{
    public interface IAuthService
    {
        AuthData GetAuthData(string id, string userName);
        string HashPassword(string password);
        bool VerifyPassword(string actualPassword, string hashedPassword);
    }
}
