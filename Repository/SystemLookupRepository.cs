using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dagagino.Data;
using Dagagino.Dto.LookupDtos;
using Dagagino.Interfaces;
using Dagagino.Mappers;
using Dagagino.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using static Dagagino.Dto.LookupDtos.GovernorateDtos;

namespace Dagagino.Repository
{
    public class SystemLookupRepository(AppDBContext dBContext) : ISystemLookupRepository
    {
        private readonly AppDBContext _dBContext = dBContext;
        /* -------------------------------------------------------------------------- */
        /*                            Get All Governorates                            */
        /* -------------------------------------------------------------------------- */
        public async Task<List<GovernorateDto>> GetGovernorates()
        {
            var governorates = await _dBContext.Governorates.Find(new BsonDocument()).ToListAsync();

            var governoratesDto = governorates.Select(gov => gov.ToGovernorateDto()).ToList();
            
            return governoratesDto;
        }

        /* -------------------------------------------------------------------------- */
        /*                         Get Governorate State By Id                        */
        /* -------------------------------------------------------------------------- */
        public async Task<GovernorateStateDto> GetGovernorateState(string id){
            var state = await _dBContext.GovernorateStates
            .Find( item => item.Id.ToString() == id)
            .FirstOrDefaultAsync() ?? throw new Exception("State Not Exist");

            return state.ToGovernorateStateDto();
            
        }

    }
}