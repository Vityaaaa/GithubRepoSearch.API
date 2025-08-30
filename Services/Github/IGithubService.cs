using GithubRepoSearch.ModelsDTO;

namespace GithubRepoSearch.Services.Github
{
    public interface IGithubService
    {
        Task<GithubSearchRepoResponseDTO> SearchRepositoriesAsync(string query);
    }
}
