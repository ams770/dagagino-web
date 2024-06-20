using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dagagino.Data;
using Dagagino.Dto.LookupDtos;
using Dagagino.Mappers;
using Dagagino.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using static Dagagino.Dto.LookupDtos.GovernorateDtos;

namespace Dagagino.Controllers
{

    [Route("api/[controller]")]
    public class LookupController(AppDBContext dBContext) : ControllerBase
    {
        private readonly AppDBContext _dBContext = dBContext;

        /* -------------------------------------------------------------------------- */
        /*                            Get All Governorates                            */
        /* -------------------------------------------------------------------------- */
        [HttpGet("Governorates")]
        public async Task<IActionResult> GetGovernorates()
        {
            var governorates = await _dBContext.Governorates.Find(new BsonDocument()).ToListAsync();

            var governoratesDto = governorates.Select(gov => gov.ToGovernorateDto());
            
            return Ok(governoratesDto);
        }


        
        [HttpPost("Governorates")]
        public async Task<IActionResult> CreateGovernorates([FromBody] List<GovernorateDto> data)
        {
            await _dBContext.Governorates.DeleteManyAsync(item => true);
            await _dBContext.GovernorateStates.DeleteManyAsync(item => true);
            // return Ok();

            var governorates = new List<Governorate>();

            foreach (var element in data)
            {
                var govId = new ObjectId(element.Id.ToString());
                var governorateStates = new List<GovernorateState>();
                var governorateStatesIds = new List<string>();

                foreach (var state in element.States)
                {
                    // var stateId = new ObjectId(state.Id.ToString());
                    governorateStatesIds.Add(state.Id);

                    var s = new GovernorateState
                    {
                        Id = state.Id,
                        ArName = state.ArName,
                        EnName = state.EnName,
                        Governorate = state.Id.ToString(),
                    };

                    governorateStates.Add(s);
                }

                await _dBContext.GovernorateStates.InsertManyAsync(governorateStates);

                var gov = new Governorate
                {
                    Id = element.Id.ToString(),
                    ArName = element.ArName,
                    EnName = element.EnName,
                    States = governorateStates,
                };

                governorates.Add(gov);
            }

            await _dBContext.Governorates.InsertManyAsync(governorates);
            return Ok(governorates);
        }
    }

}