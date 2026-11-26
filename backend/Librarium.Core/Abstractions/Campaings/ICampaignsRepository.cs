using Librarium.Core.Models;

namespace Librarium.DataAccess.Repositories
{
    public interface ICampaignsRepository
    {
        Task<Guid> Create(Campaign campaign, string? userId);
        Task<bool> Delete(Guid id, string? userId);
        Task<List<Campaign>> Get(string? userId);
        Task<Campaign?> GetById(Guid id, string? userId);
        Task<bool> Update(Guid id, string? userId, string name, string description);
        Task<bool> Join(Guid id, string? userId);
        Task<bool> AddChar(Guid campaignId, Guid characterId, string? userId);
    }
}