using FluentValidation;
using GithubRepoSearch.ModelsDTO;
using GithubRepoSearch.Services.Auth;
using GithubRepoSearch.Services.BookmarkSessionStore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GithubRepoSearch.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookmarksController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IBookmarkSessionStore _bookmarkSessionStore;

        public BookmarksController(IAuthService authService, IBookmarkSessionStore bookmarkSessionStore)
        {
            _authService = authService;
            _bookmarkSessionStore = bookmarkSessionStore;
        }

        [HttpGet]
        public async Task<IActionResult> GetBookmarks()
        {
            string userId = _authService.RetriveUserId(User);
            var bookmarks = await _bookmarkSessionStore.GetBookmarksAsync(userId);
            return Ok(bookmarks);
        }

        [HttpPost]
        public async Task<IActionResult> AddBookmark([FromBody] Bookmark bookmark, IValidator<Bookmark> validator)
        {
            var validatorResults = validator.Validate(bookmark);
            if (!validatorResults.IsValid) return BadRequest(validatorResults.ToDictionary());

            string userId = _authService.RetriveUserId(User);
            await _bookmarkSessionStore.AddBookmarkAsync(userId, bookmark);
            return Ok();
        }
    }
}
