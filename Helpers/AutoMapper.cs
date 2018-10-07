using AutoMapper;
using MementoScraperApi.Models;

namespace MementoScraperApi.Helpers {
    public class AutoMapperProfile : Profile {
        public AutoMapperProfile() {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}