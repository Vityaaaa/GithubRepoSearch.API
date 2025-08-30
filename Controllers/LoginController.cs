using Microsoft.AspNetCore.Mvc;
using GithubRepoSearch.Models;
using GithubRepoSearch.Services.Auth;
using FluentValidation;

namespace GithubRepoSearch.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IAuthService _authService;
        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request, IValidator<LoginRequest> validator)
        {
            var validatorResults = validator.Validate(request);
            if (!validatorResults.IsValid) return BadRequest(validatorResults.ToDictionary());

            var token = _authService.Login(request.Username, request.Password);

            if (string.IsNullOrEmpty(token)) return Unauthorized();

            return Ok(new { token });
        }
    }
}
