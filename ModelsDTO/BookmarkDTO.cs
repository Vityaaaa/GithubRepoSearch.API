using FluentValidation;

namespace GithubRepoSearch.ModelsDTO
{
    public class Bookmark : RepositoryDTO
    {
    }
    public class BookmarkValidator : AbstractValidator<Bookmark>
    {
        public BookmarkValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .MaximumLength(128);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(x => x.Url)
                .NotEmpty()
                .MaximumLength(512)
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute));

            RuleFor(x => x.OwnerAvatarUrl)
                .NotEmpty()
                .MaximumLength(512)
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute));
        }
    }
}
