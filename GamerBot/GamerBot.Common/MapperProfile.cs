using AutoMapper;
using GamerBot.Common.ViewModels;
using GamerBot.Common.ViewModels.UserEditViewModels;
using GamerBot.Model.Models;

namespace GamerBot.Common;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<UserCreateViewModel, User>();

        CreateMap<User, TeammateViewModel>();
    }
}