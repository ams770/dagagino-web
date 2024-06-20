using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dagagino.Dto.AccountDtos;

namespace Dagagino.Interfaces
{
    public interface IAuthenticationRepository
    {
        public Task<string?> Register(RegisterDto registerDto);
        public Task<string?> Login(LoginDto loginDto);

    }
}