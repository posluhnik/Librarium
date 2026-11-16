using Librarium.Core.Models;

namespace Librarium.Application.Services
{
    public interface ICampaignsService
    {
        Task<Campaign?> CreateCampaign(Campaign campaign, string ownerId);
        Task<Campaign?> DeleteCampaign(Guid id, string ownerId);
        Task<List<Campaign>> GetAllCampaigns(string userId);
        Task<Campaign?> GetCampaignById(Guid id, string userId);
        Task<Campaign?> UpdateCampaign(Guid id, string ownerId, string name, string description);
        Task<Campaign?> JoinCampaign(Guid id, string userId);
    }
}