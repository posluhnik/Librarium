using Librarium.Core.Models;
using Librarium.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Librarium.Application.Services
{
    public class CampaignsService : ICampaignsService
    {
        private readonly ICampaignsRepository _campaginsRepository;

        public CampaignsService(ICampaignsRepository campaginsRepository)
        {
            _campaginsRepository = campaginsRepository;
        }

        public async Task<List<Campaign>> GetAllCampaigns(string userId)
        {
            return await _campaginsRepository.Get(userId);
        }

        public async Task<Campaign> GetCampaignById(Guid id, string userId)
        {
            return await _campaginsRepository.GetById(id, userId);
        }

        public async Task<Guid> CreateCampaign(Campaign campaign, string ownerId)
        {
            return await _campaginsRepository.Create(campaign, ownerId);
        }

        public async Task<Guid> UpdateCampaign(Guid id, string ownerId, string name, string description)
        {
            return await _campaginsRepository.Update(id, ownerId, name, description);
        }

        public async Task<Guid> DeleteCampaign(Guid id, string ownerId)
        {
            return await _campaginsRepository.Delete(id, ownerId);
        }
        public async Task<Guid> JoinCampaign(Guid id, string userId)
        {
            return await _campaginsRepository.Join(id, userId);
        }
    }
}
