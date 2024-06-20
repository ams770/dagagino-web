using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dagagino.Models;
using static Dagagino.Dto.LookupDtos.GovernorateDtos;

namespace Dagagino.Mappers
{
    public static class LookupMappers
    {
         public static GovernorateDto ToGovernorateDto(this Governorate governorate)
        {
            return new GovernorateDto
            {
             Id = governorate.Id.ToString(), 
             ArName = governorate.ArName,
             EnName = governorate.EnName,
             States = governorate.States.Select(item => item.ToGovernorateStateDto()).ToList(),
            };
        }
         public static GovernorateStateDto ToGovernorateStateDto(this GovernorateState governorate)
        {
            return new GovernorateStateDto
            {
             Id = governorate.Id.ToString(), 
             ArName = governorate.ArName,
             EnName = governorate.EnName,             
            };
        }
    }
}