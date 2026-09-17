using Librarium.Core.Models;

namespace Librarium.DataAccess.Repositories
{
    public interface ICampaignsRepository
    {
        Task<Guid> Create(Campaign campaign);
        Task<Guid> Delete(Guid id);
        Task<List<Campaign>> Get();
        Task<Guid> Update(Guid id, string name, string description);
    }
}
