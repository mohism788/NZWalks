using NZWalks.API.Model.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {

        Task<List<Region>> GetAllRegionsAsync();
        Task<Region?> GetRegionByIdAsync(Guid id);

        Task<Region> CreateNewRegionAsync(Region region);

        Task<Region?> UpdateRegionAsync(Guid id, Region region);

        Task<Region?> DeleteRegionAsync(Guid id);

    }
}
