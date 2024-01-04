using AutoMapper;
using WordWave.Api.Common.ViewModels.Queries;
using WordWave.Api.Domain.Models;

namespace WordWave.Api.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, LoginUserViewModel>()
            .ReverseMap();
        
    }
}