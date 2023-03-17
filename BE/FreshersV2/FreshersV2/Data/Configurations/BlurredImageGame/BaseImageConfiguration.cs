using FreshersV2.Data.Models.BlurredImageGame;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshersV2.Data.Configurations.BlurredImageGame
{
    public class BaseImageConfiguration : IEntityTypeConfiguration<BaseImage>
    {
        public void Configure(EntityTypeBuilder<BaseImage> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
               .Property(x => x.Id)
               .ValueGeneratedOnAdd();
        }
    }
}
