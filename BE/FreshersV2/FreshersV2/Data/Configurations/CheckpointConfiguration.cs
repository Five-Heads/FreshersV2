using FreshersV2.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshersV2.Data.Configurations
{
    public class CheckpointConfiguration : IEntityTypeConfiguration<Checkpoint>
    {
        public void Configure(EntityTypeBuilder<Checkpoint> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasOne(x => x.TreasureHunt)
                .WithMany(th => th.Checkpoints)
                .HasForeignKey(x => x.TreasureHuntId);
        }
    }
}
