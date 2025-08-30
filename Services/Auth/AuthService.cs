using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GithubRepoSearch.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateJwtToken()
        {
            string secret = _config["Jwt:Secret"];
            int expiresIn = _config.GetValue<int>("Jwt:ExpiresInMinutes");
            string issuer = _config["Jwt:Issuer"];
            string audience = _config["Jwt:Audience"];

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString())
                ]),
                Expires = DateTime.UtcNow.AddMinutes(expiresIn),
                SigningCredentials = credentials,
                Issuer = issuer,
                Audience = audience,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string Login(string username, string password)
        {
            // For demo, any username/password is accepted
            return GenerateJwtToken();
        }
        public string RetriveUserId(ClaimsPrincipal principal)
        {
            string userId =
                principal.FindFirstValue(ClaimTypes.NameIdentifier) ??
                principal.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (string.IsNullOrWhiteSpace(userId))
                throw new InvalidOperationException("Authenticated user id is missing from the token.");

            return userId;
        }
    }
}
