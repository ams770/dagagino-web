using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dagagino.Dto.AccountDtos
{
    public class LoginDto
    {
        [Required]
        public required string username { get; set; }
        
        [Required]
        public required string password { get; set; }
    }
}