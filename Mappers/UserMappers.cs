using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dagagino.Dto.AccountDtos;
using Dagagino.Models;
using DnsClient.Protocol;
using static Dagagino.Dto.LookupDtos.GovernorateDtos;

namespace Dagagino.Mappers
{
    public static class UserMappers
    {

        /* -------------------------------------------------------------------------- */
        /*                            To User From Register                           */
        /* -------------------------------------------------------------------------- */
        public static User ToUserFromRegisterDto(this RegisterDto registerDto, string salt, string hashedPassword)
        {
            return new User
            {
                ArName = registerDto.arName,
                EnName = registerDto.enName,
                Email = registerDto.email,
                Role = registerDto.role,
                Salt = salt,
                Password = hashedPassword,
                Address = registerDto.address,
                Description = registerDto.description,
                GovernorateState = registerDto.state,
            };
        }


        /* -------------------------------------------------------------------------- */
        /*                              Profile DTO Data                              */
        /* -------------------------------------------------------------------------- */
        public static AccountDto ToAccountDtoFromUser(this User user, GovernorateStateDto? state)
        {
            return new AccountDto
            {
                id = user.Id.ToString(),
                arName = user.ArName,
                enName = user.EnName,
                email = user.Email,
                role = user.Role,
                address = user.Address,
                description = user.Description,
                state = state,
            };
        }
    }
}