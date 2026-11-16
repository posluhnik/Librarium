using Librarium.Core.Models;
using Librarium.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;


namespace Librarium.DataAccess.Repositories
{
    public class CampaignsRepository : ICampaignsRepository
    {
        private readonly ApplicationDbContext _context;

        public CampaignsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Campaign>> Get(string userId)
        {
            var campaignEntities = await _context.Campaigns
                .AsNoTracking()
                .ToListAsync();

            var campaigns = campaignEntities
                .Select(b => Campaign.Create(b.Id, b.ownerId, b.Name, b.Description, b.Players).campaign)
                .Where(b => b != null && (b.ownerId == userId || b.Players.Contains(userId)))
                .ToList();

            return campaigns;
        }

        public async Task<Campaign> GetById(Guid id, string userId)
        {
            var campaignEntities = await _context.Campaigns
                .AsNoTracking()
                .ToListAsync();

            var campaigns =  campaignEntities
                .Select(b => Campaign.Create(b.Id, b.ownerId, b.Name, b.Description, b.Players).campaign)
                .Where(b => b != null && (b.ownerId == userId || b.Players.Contains(userId)) && (b.Id == id))
                .ToList();

            if (campaigns.Count != 0)
            {
                return campaigns[0];
            }

            return null;
        }

        public async Task<Campaign> Create(Campaign campaign, string ownerId)
        {
            if (!string.IsNullOrEmpty(ownerId))
            {
                var campaignEntity = new CampaignEntity
                {
                    Id = campaign.Id,
                    ownerId = ownerId,
                    Name = campaign.Name,
                    Description = campaign.Description,
                    Players = new List<string>()
                };

                await _context.Campaigns.AddAsync(campaignEntity);
                await _context.SaveChangesAsync();

                return campaign;
            }

            return null;
        }

        public async Task<Campaign> Update(Guid id, string ownerId, string name, string description)
        {
            Campaign campaignResponse = _context.Campaigns
                    .Where(b => b.Id == id && b.ownerId == ownerId)
                    .Select(b => Campaign.Create(b.Id, b.ownerId, b.Name, b.Description, b.Players).campaign)
                    .FirstOrDefault();

            if (campaignResponse != null)
            {
                await _context.Campaigns
                    .Where(b => b.Id == id)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(b => b.Name, b => name)
                        .SetProperty(b => b.Description, b => description));
            }
            
            return campaignResponse;
        }

        public async Task<Campaign> Delete(Guid id, string ownerId)
        {
            Campaign campaignResponse = _context.Campaigns
                    .Where(b => b.Id == id && b.ownerId == ownerId)
                    .Select(b => Campaign.Create(b.Id, b.ownerId, b.Name, b.Description, b.Players).campaign)
                    .FirstOrDefault();
            
            if (campaignResponse != null)
            {
                await _context.Campaigns
                    .Where(b => b.Id == id)
                    .ExecuteDeleteAsync();
            }
            
            return campaignResponse;
        }
        public async Task<Campaign> Join(Guid id, string userId)
        {
            var campaign = await _context.Campaigns.FirstOrDefaultAsync(b => b.Id == id);

            if (campaign != null && !campaign.Players.Contains(userId) && campaign.ownerId != userId && !string.IsNullOrEmpty(userId))
            {
                campaign.Players.Add(userId);
                await _context.SaveChangesAsync();
            }

            return _context.Campaigns
                .Where(b => b.Id == id)
                .Select(b => Campaign.Create(b.Id, b.ownerId, b.Name, b.Description, b.Players).campaign)
                .FirstOrDefault();
        }
    }
}
