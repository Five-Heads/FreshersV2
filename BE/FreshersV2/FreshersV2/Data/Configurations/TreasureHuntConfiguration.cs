using FreshersV2.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshersV2.Data.Configurations
{
    public class TreasureHuntConfiguration : IEntityTypeConfiguration<TreasureHunt>
    {
        public void Configure(EntityTypeBuilder<TreasureHunt> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
