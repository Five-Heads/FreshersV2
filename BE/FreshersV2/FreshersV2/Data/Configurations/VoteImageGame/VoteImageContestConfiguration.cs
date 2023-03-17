using FreshersV2.Data.Models.VoteImageGame;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshersV2.Data.Configurations.VoteImageGame
{
    public class VoteImageContestConfiguration : IEntityTypeConfiguration<VoteImageContest>
    {
        public void Configure(EntityTypeBuilder<VoteImageContest> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasMany(x => x.Participants)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(x => x.Rounds)
                .WithOne()
                .HasForeignKey(x => x.ContestId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(x => x.Images)
                .WithOne()
                .HasForeignKey(x => x.ContestId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(x => x.UserContests)
                .WithOne()
                .HasForeignKey(x => x.ContestId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
/*

        public List<UserContest> UserContests { get; set; } = new List<UserContest>();

*/