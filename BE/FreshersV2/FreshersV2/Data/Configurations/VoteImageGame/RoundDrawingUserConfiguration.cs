using FreshersV2.Data.Models.VoteImageGame;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshersV2.Data.Configurations.VoteImageGame
{
    public class RoundDrawingUserConfiguration : IEntityTypeConfiguration<RoundDrawingUser>
    {
        public void Configure(EntityTypeBuilder<RoundDrawingUser> builder)
        {
            builder
                .HasKey(x => new { x.UserId, x.RoundId });

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.RoundsDrawing)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Round)
                .WithMany(x => x.DrawingUsers)
                .HasForeignKey(x => x.RoundId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
