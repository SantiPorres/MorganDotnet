using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ProjectConfig : BaseEntityConfig<Project>
    {
        public override void Configure(EntityTypeBuilder<Project> builder)
        {
            base.Configure(builder);

            builder.ToTable("Projects");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(x => x.Description)
                .HasMaxLength(300);

            builder.HasOne(x => x.User)
                .WithMany(p => p.OwnsProjects)
                .HasForeignKey(x => x.OwnerId)
                .IsRequired();

            builder.HasMany(x => x.ProjectUsers)
                .WithOne(p => p.Project);

            builder.HasMany(x => x.Tasks)
                .WithOne(p => p.Project);
        }
    }
}
