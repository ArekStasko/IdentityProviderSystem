using AutoMapper;
using IdentityProviderSystem.Domain.DTO;
using IdentityProviderSystem.Domain.Requests.LoginUser;
using IdentityProviderSystem.Domain.Requests.RegisterUser;

namespace IdentityProviderSystem.mapper;

public class ControllersMapperProfile : Profile
{
    public ControllersMapperProfile()
    {
        CreateMap<LoginUser, UserDTO>()
            .ForMember(u => u.Username, opts => opts.MapFrom(src => src.Username))
            .ForMember(u => u.Hash, opts => opts.MapFrom(src => src.Hash));
        
        CreateMap<RegisterUser, UserDTO>()
            .ForMember(u => u.Username, opts => opts.MapFrom(src => src.Username))
            .ForMember(u => u.Hash, opts => opts.MapFrom(src => src.Hash));
    }
}