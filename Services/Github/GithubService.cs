using AutoMapper;
using GithubRepoSearch.Models;
using GithubRepoSearch.ModelsDTO;
using System.Text.Json;

namespace GithubRepoSearch.Services.Github
{
    public class GithubService : IGithubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;
        public GithubService(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
        }

        public async Task<GithubSearchRepoResponseDTO> SearchRepositoriesAsync(string query)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("accept","application/vnd.github+json");// gitHub recommends to add this header
            client.DefaultRequestHeaders.Add("User-Agent", "GithubRepoSearch");

            var url = $"https://api.github.com/search/repositories?q={query}";
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string responseContent = await response.Content.ReadAsStringAsync();
            GithubSearchRepoResponse? result = JsonSerializer.Deserialize<GithubSearchRepoResponse>(responseContent);

            GithubSearchRepoResponseDTO resultDTO = _mapper.Map<GithubSearchRepoResponseDTO>(result);

            return resultDTO;
        }
    }
}
