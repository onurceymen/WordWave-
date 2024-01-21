using AutoMapper;
using WordWave.Api.Common.ViewModels.Queries;
using WordWave.Api.Common.ViewModels.RequestModels;
using WordWave.Api.Domain.Models;

namespace WordWave.Api.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, LoginUserViewModel>()
            .ReverseMap();
        CreateMap<CreateUserCommand, User>();

        CreateMap<UpdateUserCommand, User>();
    }
}