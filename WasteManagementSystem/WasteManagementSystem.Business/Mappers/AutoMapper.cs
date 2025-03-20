using AutoMapper;
using System.Reflection.Metadata;
using WasteManagementSystem.Business.DTO;
using WasteManagementSystem.Data.Models;

namespace WasteManagementSystem.Business.Mappers;
public class AutoMapper:Profile
{
    public AutoMapper()
    {
        CreateMap<UserType, UserTypeDto>().ReverseMap();
        CreateMap<WasteType, WasteTypeDto>().ReverseMap();
    }
}

