using FreshersV2.Data.Models.VoteImageGame;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshersV2.Data.Configurations.VoteImageGame
{
    public class VoteImageConfiguration : IEntityTypeConfiguration<VoteImage>
    {
        public void Configure(EntityTypeBuilder<VoteImage> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.Images)
                .HasForeignKey(x =>
                new
                {
                    x.UserId,
                    x.ContestId
                })
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.VoteImageRound)
                .WithMany(x => x.Images)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Contest)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.ContestId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}