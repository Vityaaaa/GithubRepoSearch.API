using System.Security.Claims;

namespace GithubRepoSearch.Services.Auth
{
    public interface IAuthService
    {
        string GenerateJwtToken();
        string Login(string username, string password);
        string RetriveUserId(ClaimsPrincipal principal);
    }
}
