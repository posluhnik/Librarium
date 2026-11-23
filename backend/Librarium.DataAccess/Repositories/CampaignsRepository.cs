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

        public async Task<List<Campaign>> Get(string? userId)
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

        public async Task<Campaign?> GetById(Guid id, string? userId)
        {
            var query = _context.Campaigns
                .Where(b => b.Id == id && b.ownerId == userId);

            return await query
                .Select(b => Campaign.Create(b.Id, b.ownerId, b.Name, b.Description, b.Players).campaign)
                .FirstOrDefaultAsync();
        }

        public async Task<Guid> Create(Campaign Campaign, string? userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var CampaignEntity = new CampaignEntity
                {
                    Id = Campaign.Id,
                    ownerId = userId,
                    Name = Campaign.Name,
                    Description = Campaign.Description,
                    Players = Campaign.Players
                };

                await _context.Campaigns.AddAsync(CampaignEntity);
                await _context.SaveChangesAsync();
            }
            
            return Campaign.Id;
        }

        public async Task<bool> Update(Guid id, string? userId, string name, string description)
        {
            var query = _context.Campaigns
                .Where(b => b.Id == id && b.ownerId == userId);

            int updatedCount = await query.ExecuteUpdateAsync(s => s
                .SetProperty(b => b.Name, name)
                .SetProperty(b => b.Description, description));

            if (updatedCount == 0)
                return false;

            return true;
        }

        public async Task<bool> Delete(Guid id, string? userId)
        {
            var query = _context.Campaigns
                .Where(b => b.Id == id && b.ownerId == userId);
            
            int deletedCount = await query.ExecuteDeleteAsync();
            
            if (deletedCount == 0)
                return false;

            return true;
        }

        public async Task<bool> Join(Guid id, string? userId)
        {
            var campaign = await _context.Campaigns.FirstOrDefaultAsync(b => b.Id == id);

            if (campaign != null && !campaign.Players.Contains(userId) && campaign.ownerId != userId && !string.IsNullOrEmpty(userId))
            {
                campaign.Players.Add(userId);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> AddCharacter(Guid campaignId, Guid characterId, string? userId)
        {
            var campaign = await _context.Campaigns.FirstOrDefaultAsync(b => b.Id == campaignId && (b.ownerId == userId || b.Players.Contains(userId)));
            var character = await _context.Characters.FirstOrDefaultAsync(b => b.Id == characterId && b.ownerId == userId);

            if (campaign != null && character != null)
            {
                campaign.Characters.Add(characterId);

                return true;
            }

            return false;
        }
    }
}
