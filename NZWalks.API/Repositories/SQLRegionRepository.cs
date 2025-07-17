using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using NZWalks.API.Data;
using NZWalks.API.Model.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _dbContext;
        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Region> CreateNewRegionAsync(Region region)
        {
            await _dbContext.Regions.AddAsync(region);
            await _dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteRegionAsync(Guid id)
        {
            var exist = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (exist == null)
            {
                return null;
            }

            _dbContext.Regions.Remove(exist);
            await _dbContext.SaveChangesAsync();

            return exist;
        }

        public async Task<List<Region>> GetAllRegionsAsync()
        {
            return await _dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetRegionByIdAsync(Guid id)
        {
            return await _dbContext.Regions.FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<Region?> UpdateRegionAsync(Guid id, Region region)
        {
            var exist = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (exist == null)
            {
                return null;
            }

            exist.Code = region.Code;
            exist.Name = region.Name;
            exist.RegionImageUrl = region.RegionImageUrl;
            await _dbContext.SaveChangesAsync();

            return exist;
        }
    }
}
