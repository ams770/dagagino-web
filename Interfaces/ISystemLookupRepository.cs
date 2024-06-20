using static Dagagino.Dto.LookupDtos.GovernorateDtos;

namespace Dagagino.Interfaces
{
    public interface ISystemLookupRepository
    {
        public Task<List<GovernorateDto>> GetGovernorates();
        public Task<GovernorateStateDto> GetGovernorateState(string id);
    }
}