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
            var CampaignEntities = await _context.Campaigns
                .AsNoTracking()
                .ToListAsync();

            var Campaigns = CampaignEntities
                .Select(b => Campaign.Create(b.Id, b.ownerId, b.Name, b.Description, b.Players).campaign)
                .Where(b => b != null && (b.ownerId == userId))
                .ToList();

            return Campaigns;
        }

        public async Task<Campaign?> GetById(Guid id, string userId)
        {
            var query = _context.Campaigns
                .Where(b => b.Id == id && b.ownerId == userId);

            return await query
                .Select(b => Campaign.Create(b.Id, b.ownerId, b.Name, b.Description, b.Players).campaign)
                .FirstOrDefaultAsync();
        }

        public async Task<Campaign?> Create(Campaign Campaign, string ownerId)
        {
            if (!string.IsNullOrEmpty(ownerId))
            {
                var CampaignEntity = new CampaignEntity
                {
                    Id = Campaign.Id,
                    ownerId = ownerId,
                    Name = Campaign.Name,
                    Description = Campaign.Description,
                    Players = Campaign.Players
                };

                await _context.Campaigns.AddAsync(CampaignEntity);
                await _context.SaveChangesAsync();

                return Campaign;
            }
            
            return null;
        }

        public async Task<Campaign?> Update(Guid id, string ownerId, string name, string description)
        {
            var query = _context.Campaigns
                .Where(b => b.Id == id && b.ownerId == ownerId);

            int updatedCount = await query.ExecuteUpdateAsync(s => s
                .SetProperty(b => b.Name, name)
                .SetProperty(b => b.Description, description));

            if (updatedCount == 0)
                return null;

            return await query
                .Select(b => Campaign.Create(
                    b.Id,
                    b.ownerId,
                    b.Name,
                    b.Description,
                    b.Players).campaign)
                .FirstOrDefaultAsync();
        }

        public async Task<Campaign?> Delete(Guid id, string ownerId)
        {
            var query = _context.Campaigns
                .Where(b => b.Id == id && b.ownerId == ownerId);
            
            int deletedCount = await query.ExecuteDeleteAsync();
            
            if (deletedCount == 0)
                return null;

            return await query
                .Select(b => Campaign.Create(
                    b.Id,
                    b.ownerId,
                    b.Name,
                    b.Description,
                    b.Players).campaign)
                .FirstOrDefaultAsync();
        }
        public async Task<Campaign?> Join(Guid id, string userId)
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
