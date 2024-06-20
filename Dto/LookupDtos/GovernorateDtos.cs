using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dagagino.Dto.LookupDtos
{
    public class GovernorateDtos
    {
        public class GovernorateDto
        {
            public required string Id { get; set; }
            public required string ArName { get; set; }
            public required string EnName { get; set; }
            public required List<GovernorateStateDto> States { get; set; }
        }

        public class GovernorateStateDto
        {
            public required string Id { get; set; }
            public required string ArName { get; set; }
            public required string EnName { get; set; }
        }
    }
}