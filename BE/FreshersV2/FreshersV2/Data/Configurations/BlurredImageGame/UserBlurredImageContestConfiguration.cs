using FreshersV2.Data.Models.BlurredImageGame;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshersV2.Data.Configurations.BlurredImageGame
{
    public class UserBlurredImageContestConfiguration : IEntityTypeConfiguration<UserBlurredImageContest>
    {
        public void Configure(EntityTypeBuilder<UserBlurredImageContest> builder)
        {
            builder
                .HasKey(x => new { x.UserId, x.BlurredImageContestId });

            builder
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.BlurredImageContest)
                .WithMany(x => x.UserBlurredImageContests)
                .HasForeignKey(x => x.BlurredImageContestId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
