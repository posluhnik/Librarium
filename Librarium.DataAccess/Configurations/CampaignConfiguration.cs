using Librarium.Core.Models;
using Librarium.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Librarium.DataAccess.Configurations
{
    public class CampaignConfiguration : IEntityTypeConfiguration<CampaignEntity>
    {
        public void Configure(EntityTypeBuilder<CampaignEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(b => b.Name)
                .HasMaxLength(Campaign.MAX_TITLE_LENGTH)
                .IsRequired();

            builder.Property(b => b.Description)
                .IsRequired();
        }
    }
}
