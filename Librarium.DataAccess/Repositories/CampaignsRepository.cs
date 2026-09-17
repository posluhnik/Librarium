using Librarium.Core.Models;
using Librarium.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Librarium.DataAccess.Repositories
{
    public class CampaignsRepository : ICampaignsRepository
    {
        private readonly CampaignDbContext _context;

        public CampaignsRepository(CampaignDbContext context)
        {
            _context = context;
        }

        public async Task<List<Campaign>> Get()
        {
            var campaignEntities = await _context.Campaigns
                .AsNoTracking()
                .ToListAsync();

            var campaigns = campaignEntities
                .Select(b => Campaign.Create(b.Id, b.Name, b.Description).campaign)
                .ToList();

            return campaigns;
        }

        public async Task<Guid> Create(Campaign campaign)
        {
            var campaignEntity = new CampaignEntity
            {
                Id = campaign.Id,
                Name = campaign.Name,
                Description = campaign.Description,
            };

            await _context.Campaigns.AddAsync(campaignEntity);
            await _context.SaveChangesAsync();

            return campaignEntity.Id;
        }

        public async Task<Guid> Update(Guid id, string name, string description)
        {
            await _context.Campaigns
                .Where(b => b.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.Name, b => name)
                    .SetProperty(b => b.Description, b => description));

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Campaigns
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
