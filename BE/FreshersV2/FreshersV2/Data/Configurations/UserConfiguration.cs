using FreshersV2.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreshersV2.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // TODO: might have value if two users have null as value?
            builder
                .HasIndex(x => x.FacultyNumber)
                .IsUnique();

            builder
                .Property(x => x.GroupId)
                .IsRequired(false);
        }
    }
}
