using NZWalks.API.Model.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateNewWalkAsync(Walk walk);

        Task<Walk?> GetWalkByIdAsync(Guid id);
        Task<List<Walk>> GetAllWalksAsync(string? filterOn = null,  string? filterQuery = null,
                                            string? sortBy =null,   bool? isAscending = true,
                                            int pageNumber = 1,     int pageSize = 1000);

        Task<Walk?> UpdateWalkAsync(Guid id,Walk walk);

        Task<Walk?> DeleteWalkAsync(Guid id);
    }
}
