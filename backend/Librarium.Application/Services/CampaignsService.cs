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

        public async Task<List<Campaign>> GetAllCampaigns(string? userId)
        {
            return await _campaginsRepository.Get(userId);
        }

        public async Task<Campaign?> GetCampaignById(Guid id, string? userId)
        {
            return await _campaginsRepository.GetById(id, userId);
        }

        public async Task<Guid> CreateCampaign(Campaign campaign, string? userId)
        {
            return await _campaginsRepository.Create(campaign, userId);
        }   

        public async Task<bool> UpdateCampaign(Guid id, string? userId, string name, string description)
        {
            return await _campaginsRepository.Update(id, userId, name, description);
        }

        public async Task<bool> DeleteCampaign(Guid id, string? userId)
        {
            return await _campaginsRepository.Delete(id, userId);
        }
        public async Task<bool> JoinCampaign(Guid id, string? userId)
        {
            return await _campaginsRepository.Join(id, userId);
        }
    }
}
