using Application.DTO.Security;

using AutoMapper;

using Domain.Security.Entities;

using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Security.Profiles
{
    public class UsersProfile : Profile
    {

        public UsersProfile()
        {
            CreateMap<Users, CredentialDTO>();
            var oUserDTOMap = CreateMap<Users, UsersDTO>();
            oUserDTOMap.ForMember(x => x.UserId, x => x.MapFrom(y => y.UserId.TrimEnd()));
        }

    }
}
