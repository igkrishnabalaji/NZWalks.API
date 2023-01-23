using NZWalks.API.Models.Domains;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();

        Task<Region> GetAsyncRegion(Guid Id);

        Task<Region> AddRegion(Region region);

        Task<Region> DeleteRegion(Guid RegionId);

        Task<Region> UpdateRegion(Guid Id, Region region);
    }
}