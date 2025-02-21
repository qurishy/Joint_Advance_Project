using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data_Transfer_API.Model;
using Data_Transfer_API.Model.DTOClasses;

namespace Data_Transfer_API.DTO
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<User_Info, UserInfoDTO>();

            CreateMap<UserInfoDTO, User_Info>();
        }
    }
}