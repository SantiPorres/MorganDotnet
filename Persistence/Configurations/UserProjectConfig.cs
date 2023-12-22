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

            builder.HasOne(x => x.User)
                .WithMany(p => p.UserProjects)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(x => x.Project)
                .WithMany(p => p.ProjectUsers)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Property(x => x.Role)
                .IsRequired();
        }
    }
}
