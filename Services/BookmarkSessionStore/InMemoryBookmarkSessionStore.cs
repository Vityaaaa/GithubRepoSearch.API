using GithubRepoSearch.ModelsDTO;
using System.Collections.Concurrent;

namespace GithubRepoSearch.Services.BookmarkSessionStore
{
    public class InMemoryBookmarkSessionStore : IBookmarkSessionStore
    {
        private readonly ConcurrentDictionary<string, List<Bookmark>> _store = new();

        public Task AddBookmarkAsync(string userId, Bookmark bookmark)
        {
            var list = _store.GetOrAdd(userId, _ => new List<Bookmark>());

            lock (list) // protect list against concurrent access
            {
                if (!list.Any(b => b.Id == bookmark.Id)) // prevent duplicates
                {
                    list.Add(bookmark);
                }
            }

            return Task.CompletedTask;
        }

        public Task<List<Bookmark>> GetBookmarksAsync(string userId)
        {
            _store.TryGetValue(userId, out var list);
            return Task.FromResult<List<Bookmark>>(list ?? new List<Bookmark>());
        }
    }
}
