using FreshersV2.Data.Models.VoteImageGame;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshersV2.Data.Configurations.VoteImageGame
{
    public class UserContestConfiguration : IEntityTypeConfiguration<UserContest>
    {
        public void Configure(EntityTypeBuilder<UserContest> builder)
        {
            builder.HasKey(x => new { x.UserId, x.ContestId });

            builder
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x=>x.UserId);

            builder
                .HasOne(x => x.Contest)
                .WithMany()
                .HasForeignKey(x => x.ContestId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => new { Id=x.UserId })
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasMany(x => x.Images)
                .WithOne(x => x.User)
                .HasForeignKey(x => new { x.UserId, x.ContestId })
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(x => x.RoundsDrawing)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}