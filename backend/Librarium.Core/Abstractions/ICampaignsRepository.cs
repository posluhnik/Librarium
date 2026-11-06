using Librarium.Core.Models;

namespace Librarium.DataAccess.Repositories
{
    public interface ICampaignsRepository
    {
        Task<Guid> Create(Campaign campaign, string ownerId);
        Task<Guid> Delete(Guid id, string ownerId);
        Task<List<Campaign>> Get(string userId);
        Task<Guid> Update(Guid id, string ownerId, string name, string description);
        Task<Guid> Join(Guid id, string userId);
    }
}
