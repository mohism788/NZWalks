using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using NZWalks.API.Data;
using NZWalks.API.Model.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext _dbContext;
        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Walk> CreateNewWalkAsync(Walk walk)
        {
             await _dbContext.Walks.AddAsync(walk);
             await _dbContext.SaveChangesAsync();
             return walk;
        }

        public async Task<Walk?> DeleteWalkAsync(Guid id)
        {
            var exist = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (exist == null)
            {
                return null;
            }
            _dbContext.Remove(exist);
            await _dbContext.SaveChangesAsync();
            return exist;

        }

        public async Task<List<Walk>> GetAllWalksAsync(string? filterOn = null, string? filterQuery = null)
        {
            var walks = _dbContext.Walks
                .Include(x => x.Difficulty)
                .Include(x => x.Region)
                .AsQueryable();

            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }

                if (filterOn.Equals("Distance", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.LengthInKm == double.Parse(filterQuery));
                }

                if (filterOn.Equals("Difficulty", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x=>x.Difficulty.Name.Equals(filterQuery));
                }
                if (filterOn.Equals("Region", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Region.Name.Contains(filterQuery));
                }
                if (filterOn.Equals("Region Code", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Region.Code.Contains(filterQuery));
                }

            }

            return await walks.ToListAsync();

            //return await _dbContext.Walks
            //    .Include(x=>x.Difficulty)
            //    .Include(x=>x.Region)
            //    .ToListAsync();

        }

        public async Task<Walk?> GetWalkByIdAsync(Guid id)
        {
            
            return await _dbContext.Walks
                .Include(x=>x.Difficulty)
                .Include(x=>x.Region)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateWalkAsync(Guid id, Walk walk)
        {
            var exist = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (exist == null)
            {
                return null;
            }

            exist.Description= walk.Description;
            exist.Name= walk.Name;
            exist.WalkImageUrl= walk.WalkImageUrl;
            exist.LengthInKm  = walk.LengthInKm;
            exist.RegionId= walk.RegionId;  
            exist.DifficultyId= walk.DifficultyId;
            await _dbContext.SaveChangesAsync();
            return exist;


        }
    }
}
