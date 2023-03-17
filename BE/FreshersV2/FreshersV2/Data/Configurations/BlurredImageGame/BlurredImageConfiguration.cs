using FreshersV2.Data.Models.BlurredImageGame;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshersV2.Data.Configurations.BlurredImageGame
{
    public class BlurredImageConfiguration : IEntityTypeConfiguration<BlurredImage>
    {
        public void Configure(EntityTypeBuilder<BlurredImage> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
               .Property(x => x.Id)
               .ValueGeneratedOnAdd();

            builder
               .HasOne(x => x.BaseImage)
               .WithMany(y => y.BlurredImages)
               .HasForeignKey(x => x.BaseImageId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
