using AutoMapper;
using GithubRepoSearch.Models;
using GithubRepoSearch.ModelsDTO;

namespace GithubRepoSearch.AutoMapper.Profiles
{
    public class AutoMapperGutHubRepoConfig: Profile
    {
        public AutoMapperGutHubRepoConfig()
        {
            CreateMap<GithubSearchRepoResponse, GithubSearchRepoResponseDTO>()
                .ForMember(dest => dest.Repositories, opt => opt.MapFrom(src => src.items));

            CreateMap<Repository, RepositoryDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.OwnerAvatarUrl, opt => opt.MapFrom(src =>  src.owner.avatar_url))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.html_url));
        }
    }
}
