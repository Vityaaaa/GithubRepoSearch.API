using GithubRepoSearch.ModelsDTO;

namespace GithubRepoSearch.Services.BookmarkSessionStore
{
    public interface IBookmarkSessionStore
    {
        Task AddBookmarkAsync(string userId, Bookmark bookmark);
        Task<List<Bookmark>> GetBookmarksAsync(string userId);
    }
}
