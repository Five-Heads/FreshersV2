using FreshersV2.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshersV2.Data.Configurations
{
    public class GroupTreasureHuntConfiguration : IEntityTypeConfiguration<GroupTreasureHunt>
    {
        public void Configure(EntityTypeBuilder<GroupTreasureHunt> builder)
        {
            builder
                .HasKey(x => new { x.TreasureHuntId, x.GroupId });

            builder
                .HasOne(x => x.TreasureHunt)
                .WithMany()
                .HasForeignKey(x => x.TreasureHuntId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Group)
                .WithMany(g => g.TreasureHunts)
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Next)
                .WithMany()
                .HasForeignKey(x => x.NextId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
