using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Dagagino.Services;

namespace Dagagino.Dto.AccountDtos
{
    public class UpdateAccountDto
    {
        public string? arName { get; set; }

        public string? enName { get; set; }

        public string? address { get; set; }

        public string? description { get; set; }
        public string? state { get; set; }
    }
}