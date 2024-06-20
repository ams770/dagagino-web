using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dagagino.Dto.AccountDtos;
using Dagagino.Models;
using MongoDB.Driver;

namespace Dagagino.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> GetById(string id);
        public Task<User> UpdateUser(string id, UpdateAccountDto updateDto);
    }
}