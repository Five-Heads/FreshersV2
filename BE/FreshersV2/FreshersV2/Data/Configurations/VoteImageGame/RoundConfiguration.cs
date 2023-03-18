
using FreshersV2.Data.Models.VoteImageGame;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshersV2.Data.Configurations.VoteImageGame
{
    public class RoundConfiguration : IEntityTypeConfiguration<Round>
    {
        public void Configure(EntityTypeBuilder<Round> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasOne(x => x.Contest)
                .WithMany(x => x.Rounds)
                .HasForeignKey(x => x.ContestId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(x => x.Votes)
                .WithOne(x => x.Round)
                .HasForeignKey(x => x.RoundId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(x => x.DrawingUsers)
                .WithOne(x => x.Round)
                .HasForeignKey(x => x.RoundId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}