using Librarium.Core.Models;

namespace Librarium.Application.Services
{
    public interface ICampaignsService
    {
        Task<Guid> CreateCampaign(Campaign campaign);
        Task<Guid> DeleteCampaign(Guid id);
        Task<List<Campaign>> GetAllCampaigns();
        Task<Guid> UpdateCampaign(Guid id, string name, string description);
    }
}
