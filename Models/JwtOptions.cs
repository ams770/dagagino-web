using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dagagino.Models
{
    public class JwtOptions
    {
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public required string Lifetime { get; set; }
        public required string SigningKey { get; set; }
    }
}