using FluentValidation;

namespace GithubRepoSearch.Models
{
    public class SearchRequest
    {
        public string Query { get; set; }
    }
    public class SearchRequestValidator : AbstractValidator<SearchRequest>
    {
        public SearchRequestValidator()
        {
            RuleFor(x => x.Query)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(256);
        }
    }
}
