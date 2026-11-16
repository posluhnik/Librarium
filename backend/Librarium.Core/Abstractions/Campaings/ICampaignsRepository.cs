using Librarium.Core.Models;

namespace Librarium.DataAccess.Repositories
{
    public interface ICampaignsRepository
    {
        Task<Campaign?> Create(Campaign campaign, string ownerId);
        Task<Campaign?> Delete(Guid id, string ownerId);
        Task<List<Campaign>> Get(string userId);
        Task<Campaign?> GetById(Guid id, string userId);
        Task<Campaign?> Update(Guid id, string ownerId, string name, string description);
        Task<Campaign?> Join(Guid id, string userId);
    }
}