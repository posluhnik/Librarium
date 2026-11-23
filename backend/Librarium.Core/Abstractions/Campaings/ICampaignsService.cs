using Librarium.Core.Models;

namespace Librarium.Application.Services
{
    public interface ICampaignsService
    {
        Task<Guid> CreateCampaign(Campaign campaign, string? userId);
        Task<bool> DeleteCampaign(Guid id, string? userId);
        Task<List<Campaign>> GetAllCampaigns(string? userId);
        Task<Campaign?> GetCampaignById(Guid id, string? userId);
        Task<bool> UpdateCampaign(Guid id, string? userId, string name, string description);
        Task<bool> JoinCampaign(Guid id, string? userId);
    }
}