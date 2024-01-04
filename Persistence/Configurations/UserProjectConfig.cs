using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class UserProjectConfig : BaseEntityConfig<UserProject>
    {
        public override void Configure(EntityTypeBuilder<UserProject> builder)
        {
            base.Configure(builder);

            builder.ToTable("UserProjects");

            builder.HasOne<User>()
                .WithMany(p => p.UserProjects)
                .HasForeignKey(x => x.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<Project>()
                .WithMany(p => p.ProjectUsers)
                .HasForeignKey(x => x.ProjectId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.Role)
                .IsRequired();
        }
    }
}
