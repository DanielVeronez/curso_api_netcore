using Api.Domain.Dtos.User;
using Api.Domain.Models;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class DtosToModelsProfile : Profile
    {
        public DtosToModelsProfile()
        {
            CreateMap<UserModel, UserDto>().ReverseMap();
        }
    }
}
