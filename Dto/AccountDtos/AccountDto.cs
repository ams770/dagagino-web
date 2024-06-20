using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dagagino.Models;
using static Dagagino.Dto.LookupDtos.GovernorateDtos;

namespace Dagagino.Dto.AccountDtos
{
    public class AccountDto
    {
        public required string id { get; set; }
        public required string arName { get; set; }
        public required string enName { get; set; }
        public required string email { get; set; }
        public required string role { get; set; }
        public required string address { get; set; }
        public string? description { get; set; }
        public GovernorateStateDto? state { get; set; }
    }
}