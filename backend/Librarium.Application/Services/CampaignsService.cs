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

        public async Task<List<Campaign>> GetAllCampaigns()
        {
            return await _campaginsRepository.Get();
        }

        public async Task<Guid> CreateCampaign(Campaign campaign, string ownerId)
        {
            return await _campaginsRepository.Create(campaign, ownerId);
        }

        public async Task<Guid> UpdateCampaign(Guid id, string name, string description)
        {
            return await _campaginsRepository.Update(id, name, description);
        }

        public async Task<Guid> DeleteCampaign(Guid id)
        {
            return await _campaginsRepository.Delete(id);
        }
    }
}
