using FreshersV2.Data.Configurations;
using FreshersV2.Data.Configurations.BlurredImageGame;
using FreshersV2.Data.Configurations.VoteImageGame;
using FreshersV2.Data.Models;
using FreshersV2.Data.Models.VoteImageGame;
using FreshersV2.Data.Models.BlurredImageGame;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FreshersV2.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {

        #region TreasureHunt

        public DbSet<Checkpoint> Checkpoints { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<GroupTreasureHunt> GroupTreasureHunts { get; set; }

        public DbSet<TreasureHunt> TreasureHunts { get; set; }

        public DbSet<UserTreasureHunt> UserTreasureHunts { get; set; }

        #endregion

        #region BlurredImage

        public DbSet<BaseImage> BaseImages { get; set; }

        public DbSet<BlurredImage> BlurredImages { get; set; }

        public DbSet<BlurredImageContest> BlurredImageContests { get; set; }

        public DbSet<UserBlurredImageContest> UserBlurredImageContests { get; set; }

        #endregion

        #region VoteImage

        public DbSet<VoteImage> Images { get; set; }

        public DbSet<VoteImageContest> Contests { get; set; }

        public DbSet<UserContest> UserContests { get; set; }

        public DbSet<Round> Rounds { get; set; }

        public DbSet<RoundVote> RoundVotes { get; set; }

        public DbSet<RoundDrawingUser> RoundDrawingUsers { get; set; }
        #endregion



        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CheckpointConfiguration());
            builder.ApplyConfiguration(new GroupConfiguration());
            builder.ApplyConfiguration(new GroupTreasureHuntConfiguration());
            builder.ApplyConfiguration(new TreasureHuntConfiguration());
            builder.ApplyConfiguration(new UserTreasureHuntConfiguration());

            builder.ApplyConfiguration(new BaseImageConfiguration());
            builder.ApplyConfiguration(new BlurredImageConfiguration());
            builder.ApplyConfiguration(new BlurredImageContestConfiguration());
            builder.ApplyConfiguration(new UserBlurredImageContestConfiguration());

            builder.ApplyConfiguration(new VoteImageConfiguration());
            builder.ApplyConfiguration(new VoteImageContestConfiguration());
            builder.ApplyConfiguration(new UserContestConfiguration());
            builder.ApplyConfiguration(new RoundVoteConfiguration());
            builder.ApplyConfiguration(new RoundConfiguration());
            builder.ApplyConfiguration(new RoundDrawingUserConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
