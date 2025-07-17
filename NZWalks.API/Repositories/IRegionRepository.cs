using NZWalks.API.Model.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {

        Task<List<Region>> GetAllRegionsAsync(string? filterOn = null, string? filterQuery = null, bool? isAscending = true);
        Task<Region?> GetRegionByIdAsync(Guid id);

        Task<Region> CreateNewRegionAsync(Region region);

        Task<Region?> UpdateRegionAsync(Guid id, Region region);

        Task<Region?> DeleteRegionAsync(Guid id);

    }
}
