using Microsoft.EntityFrameworkCore;
using NZWalks.API.Controllers;
using NZWalks.API.Data;
using NZWalks.API.Models.Domains;
//using NZWalks.API.Models.Dto;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDBContext nZWalksDbContext;
        public RegionRepository(NZWalksDBContext nZWalksDBContext)
        {
            this.nZWalksDbContext = nZWalksDBContext;
        }

        public async Task<Region> AddRegion(Region region)
        {
            region.Id = Guid.NewGuid();
            await nZWalksDbContext.AddAsync(region);
            await nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteRegion(Guid RegionId)
        {
            var regionDelete = nZWalksDbContext.Regions.FirstOrDefault(x => x.Id == RegionId);
            if (regionDelete == null)
            {
                return null;
            }
            nZWalksDbContext.Regions.Remove(regionDelete);
            await nZWalksDbContext.SaveChangesAsync();
            return regionDelete;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await nZWalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsyncRegion(Guid Id)
        {
           return await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Region> UpdateRegion(Guid id, Region region)
        {
            var regionUpdate = nZWalksDbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionUpdate == null)
            {
                return null;
            }
            regionUpdate.Code = region.Code;
            regionUpdate.Area = region.Area;
            regionUpdate.Lat = region.Lat;
            regionUpdate.Long = region.Long;
            regionUpdate.Name = region.Name;
            regionUpdate.Population = region.Population;
            await nZWalksDbContext.SaveChangesAsync();
            return regionUpdate;
        }
    }
}
