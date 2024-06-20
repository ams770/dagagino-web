using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dagagino.Data;
using Dagagino.Dto.AccountDtos;
using Dagagino.Interfaces;
using Dagagino.Models;
using MongoDB.Driver;

namespace Dagagino.Repository
{
    public class UserRepository(AppDBContext dBContext) : IUserRepository
    {
        private readonly IMongoCollection<User> _users = dBContext.Users;
        private readonly IMongoCollection<GovernorateState> _governorateStates = dBContext.GovernorateStates;


        /* -------------------------------------------------------------------------- */
        /*                               Get User By ID                               */
        /* -------------------------------------------------------------------------- */
        public async Task<User> GetById(string id)
        {
            var existUser = await _users
            .Find(user => user.Id.ToString() == id)
            .FirstOrDefaultAsync() ?? throw new Exception("User not exist");

            return existUser;
        }

        /* -------------------------------------------------------------------------- */
        /*                             Update User Account                            */
        /* -------------------------------------------------------------------------- */
        public async Task<User> UpdateUser(string id, UpdateAccountDto updateDto)
        {
            var existUser = await GetById(id) ?? throw new Exception("User is not exist");

            existUser.ArName = updateDto.arName ?? existUser.ArName;
            existUser.EnName = updateDto.enName ?? existUser.EnName;
            existUser.Address = updateDto.address ?? existUser.Address;
            existUser.Description = updateDto.description ?? existUser.Description;

            if(!string.IsNullOrEmpty(updateDto.state) && updateDto.state!= existUser.GovernorateState){

                var newGovState = await _governorateStates
                .Find( item => item.Id.ToString() ==  updateDto.state)
                .FirstOrDefaultAsync();

                existUser.GovernorateState = newGovState?.Id?.ToString() ?? existUser.GovernorateState;
            }

            var update = Builders<User>.Update
            .Set(user => user.ArName, existUser.ArName)
            .Set(user => user.EnName, existUser.EnName)
            .Set(user => user.Address, existUser.Address)
            .Set(user => user.GovernorateState, existUser.GovernorateState)
            .Set(user => user.Description, existUser.Description);

            await _users.UpdateOneAsync(user => user.Id.ToString() == id, update);


            return existUser;
        }

    }
}
