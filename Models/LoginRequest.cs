using FluentValidation;

namespace GithubRepoSearch.Models
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .Length(2,12)
                .Matches("^[a-zA-Z0-9_]+$");

            RuleFor(x => x.Password)
                .NotEmpty()
                .Length(3,12)
                .Matches("^[a-zA-Z0-9+|&!*.%_?:=.-]*$");
        }
    }
}
