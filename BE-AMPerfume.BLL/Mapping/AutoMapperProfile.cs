using AutoMapper;
using BE_AMPerfume.Core.Models;
using BE_AMPerfume.Core.DTOs;

public class AutoMapperUserProfile : Profile
{
    public AutoMapperUserProfile()
    {
        CreateMap<User, UserDTO>();
        CreateMap<UserDTO, User>();
    }
}
