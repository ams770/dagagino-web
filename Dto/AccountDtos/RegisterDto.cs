using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Dagagino.Services;

namespace Dagagino.Dto.AccountDtos
{
    public class RegisterDto
    {        
        [Required]
        public required string arName { get; set; }
        
        [Required]
        public required string enName { get; set; }

        [Required]
        [EnumDataType(typeof(DagaginoUserRole))]
        public required string role { get; set; }

        [Required]
        [EmailAddress]
        public required string email { get; set; }

        [Required]
        [MinLength(8)]
        public required string password { get; set; }

        [Required]
        public required string address { get; set; }

        [Required]    
        [MinLength(24)]    
        [MaxLength(24)]    
        public required string state { get; set; }
        public string? description { get; set; }
    }
}