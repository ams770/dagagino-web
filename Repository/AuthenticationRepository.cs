using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dagagino.Data;
using Dagagino.Dto.AccountDtos;
using Dagagino.Interfaces;
using Dagagino.Mappers;
using Dagagino.Models;
using Dagagino.Services;
using MongoDB.Driver;

namespace Dagagino.Repository
{
    public class AuthenticationRepository(TokenService tokenService, HashingService hashingService, AppDBContext dBContext) : IAuthenticationRepository
    {
        private readonly TokenService _tokenService = tokenService;
        private readonly HashingService _hashingService = hashingService;
        private readonly IMongoCollection<User> _users = dBContext.Users;

        /* -------------------------------------------------------------------------- */
        /*                                    Login                                   */
        /* -------------------------------------------------------------------------- */
        public async Task<string?> Login(LoginDto loginDto)
        {
            /* ---------------------------- Check User Exist ---------------------------- */
            var existUser = await _users
            .Find(user => user.Email == loginDto.username)
            .FirstOrDefaultAsync() ?? throw new Exception("Username or password is incorrect");

            /* ----------------------------- Check Password ----------------------------- */
            var verified = _hashingService.ValidatePassword(existUser.Salt, existUser.Password, loginDto.password);
            if (!verified) throw new Exception("Username or password is incorrect");

            /* ------------------------------ Create Token ------------------------------ */
            var userToken = _tokenService.GenerateToken(existUser.Id.ToString(), GetUserRole(existUser));
            return userToken;

        }

        /* -------------------------------------------------------------------------- */
        /*                                  Register                                  */
        /* -------------------------------------------------------------------------- */
        public async Task<string?> Register(RegisterDto registerDto)
        {
            /* ---------------------------- Remove All Users ---------------------------- */
            // await _users.DeleteManyAsync(u => true);

            /* ---------------------------- Check User Exist ---------------------------- */
            var existUser = await _users
            .Find(user => user.Email == registerDto.email)
            .FirstOrDefaultAsync();

            if (existUser != null) throw new Exception("Username already exist");

            /* ------------------------------ Hash Password ----------------------------- */
            var (salt, hashedPassword) = _hashingService.HashPassword(registerDto.password);

            /* ----------------------------- Create New Doc ----------------------------- */
            var newUser = registerDto.ToUserFromRegisterDto(salt, hashedPassword);
            await _users.InsertOneAsync(newUser);
            /* ------------------------------ Create Token ------------------------------ */
            var userToken = _tokenService.GenerateToken(newUser.Id.ToString(), GetUserRole(newUser));
            return userToken;
        }

        /* -------------------------------------------------------------------------- */
        /*                               Helper Methods                               */
        /* -------------------------------------------------------------------------- */
        private static DagaginoUserRole GetUserRole(User user)
        {
            /* ------------------------------ Get User Role ----------------------------- */
            if (user.Role == DagaginoUserRole.SEllER.ToString())
            {
                return DagaginoUserRole.SEllER;
            }

            else if (user.Role == DagaginoUserRole.CLIENT.ToString())
            {
                return DagaginoUserRole.CLIENT;
            }

            throw new Exception("Invalid User Role");

        }

    }
}