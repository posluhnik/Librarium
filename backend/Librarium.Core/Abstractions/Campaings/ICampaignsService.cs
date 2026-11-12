using Librarium.Core.Models;

namespace Librarium.Application.Services
{
    public interface ICampaignsService
    {
        Task<Guid> CreateCampaign(Campaign campaign, string ownerId);
        Task<Guid> DeleteCampaign(Guid id, string ownerId);
        Task<List<Campaign>> GetAllCampaigns(string userId);
        Task<Campaign> GetCampaignById(Guid id, string userId);
        Task<Guid> UpdateCampaign(Guid id, string ownerId, string name, string description);
        Task<Guid> JoinCampaign(Guid id, string userId);
    }
}