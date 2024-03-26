using AutoMapper;
using IdentityProviderSystem.Domain.DTO;
using IdentityProviderSystem.Domain.Models.User;

namespace IdentityProviderSystem.Domain.Services.mapper;

public class ServicesMapperProfile : Profile
{
    public ServicesMapperProfile()
    {
        CreateMap<UserDTO, User>()
            .ForMember(u => u.Username, opts => opts.MapFrom(src => src.Username))
            .ForMember(u => u.Hash, opts => opts.MapFrom(src => src.Hash));
        
        CreateMap<User, UserDTO>()
            .ForMember(u => u.Username, opts => opts.MapFrom(src => src.Username))
            .ForMember(u => u.Hash, opts => opts.MapFrom(src => src.Hash));
    }
}