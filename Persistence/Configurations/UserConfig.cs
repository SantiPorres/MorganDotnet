using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class UserConfig : BaseEntityConfig<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.ToTable("Users");

            builder.Property(x => x.Username)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(130);

            builder.Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(x => x.UserProjects)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Tasks)
                .WithOne();
        }
    }
}
