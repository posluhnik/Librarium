using Librarium.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Librarium.DataAccess
{
    public class CampaignDbContext : DbContext
    {
        public CampaignDbContext(DbContextOptions<CampaignDbContext> options) 
            : base(options) 
        { 
        }

        public DbSet<CampaignEntity> Campaigns { get; set; }
    }
}
