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

        public async Task<List<Walk>> GetAllWalksAsync(string? filterOn = null, string? filterQuery = null,
                                                        string? sortBy = null, bool? isAscending = true,
                                                        int pageNumber = 1, int pageSize = 1)
        {
            var walks = _dbContext.Walks
                .Include(x => x.Difficulty)
                .Include(x => x.Region)
                .AsQueryable();
            //filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
                else if (filterOn.Equals("Distance", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.LengthInKm == double.Parse(filterQuery));
                }
                else if (filterOn.Equals("Difficulty", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x=>x.Difficulty.Name.Equals(filterQuery));
                }
                else if (filterOn.Equals("Region", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Region.Name.Contains(filterQuery));
                }
                else if (filterOn.Equals("Region Code", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Region.Code.Contains(filterQuery));
                }

            }
            //sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("name", StringComparison.OrdinalIgnoreCase))
                {
                    if (isAscending == true)
                    {
                        walks = walks.OrderBy(x => x.Name);
                    }
                    else
                    {
                        walks = walks.OrderByDescending(x => x.Name);
                    }
                }
                else if (sortBy.Equals("distance", StringComparison.OrdinalIgnoreCase))
                {
                    if (isAscending == true)
                    {
                        walks = walks.OrderBy(x => x.LengthInKm);
                    }
                    else
                    {
                        walks = walks.OrderByDescending(x => x.LengthInKm);
                    }
                }
            }

            //pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();

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
