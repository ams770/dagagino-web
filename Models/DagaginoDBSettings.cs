using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dagagino.Models
{
    public class DagaginoDBSettings : IDagaginoDBSettings
    {        
        public required string DefaultConnection { get; set; }
        public required string DBName { get; set; }
    }

    public interface IDagaginoDBSettings
    {        
        string DefaultConnection { get; set; }
        string DBName { get; set; }
    }
}