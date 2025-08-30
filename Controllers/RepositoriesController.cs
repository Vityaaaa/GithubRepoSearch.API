using FluentValidation;
using GithubRepoSearch.Models;
using GithubRepoSearch.ModelsDTO;
using GithubRepoSearch.Services.Github;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GithubRepoSearch.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RepositoriesController : ControllerBase
    {
        private readonly IGithubService _githubService;
        public RepositoriesController(IGithubService githubService)
        {
            _githubService = githubService;
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] SearchRequest request, IValidator<SearchRequest> validator)
        {
            var validatorResults = validator.Validate(request);
            if (!validatorResults.IsValid) return BadRequest(validatorResults.ToDictionary());

            GithubSearchRepoResponseDTO res = await _githubService.SearchRepositoriesAsync(request.Query);
            return Ok(res);
        }
    }
}
