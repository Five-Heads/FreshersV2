using FreshersV2.Data.Models.BlurredImageGame;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshersV2.Data.Configurations.BlurredImageGame
{
    public class BlurredImageContestConfiguration : IEntityTypeConfiguration<BlurredImageContest>
    {
        public void Configure(EntityTypeBuilder<BlurredImageContest> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
               .Property(x => x.Id)
               .ValueGeneratedOnAdd();

            builder
               .HasOne(x => x.Winner)
               .WithMany()
               .HasForeignKey(x => x.WinnerId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasOne(x => x.BaseImage)
               .WithMany(y => y.Contests)
               .HasForeignKey(x => x.BaseImageId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
