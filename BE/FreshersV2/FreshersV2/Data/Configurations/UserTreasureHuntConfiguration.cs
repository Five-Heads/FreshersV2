using FreshersV2.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshersV2.Data.Configurations
{
    public class UserTreasureHuntConfiguration : IEntityTypeConfiguration<UserTreasureHunt>
    {
        public void Configure(EntityTypeBuilder<UserTreasureHunt> builder)
        {
            builder
                .HasKey(x => new { x.UserId, x.TreasureHuntId });

            builder
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.TreasureHunt)
                .WithMany()
                .HasForeignKey(x => x.TreasureHuntId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
