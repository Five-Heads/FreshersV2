
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
                .HasForeignKey(x => x.RoundId);

            builder
                .HasOne(x => x.Image1)
                .WithOne(x => x.Vote)
                .HasForeignKey<RoundVote>(x => x.Image1Id);

            builder
                .HasOne(x=>x.Image2)
                .WithOne(x=>x.Vote)
                .HasForeignKey<RoundVote>(x=>x.Image2Id);
        }
    }
}