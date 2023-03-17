
using FreshersV2.Data.Models.VoteImageGame;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshersV2.Data.Configurations.VoteImageGame
{
    public class RoundVoteConfiguration : IEntityTypeConfiguration<RoundVote>
    {
        public void Configure(EntityTypeBuilder<RoundVote> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasOne(x => x.Round)
                .WithMany(x => x.Votes)
                .HasForeignKey(x => x.RoundId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.VoteImageRound)
                .WithOne(x => x.RoundVote)
                .HasForeignKey<RoundVote>(x => x.VoteImageRoundId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}