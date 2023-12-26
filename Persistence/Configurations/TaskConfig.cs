using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class TaskConfig : BaseEntityConfig<Domain.Entities.Task>
    {
        public override void Configure(EntityTypeBuilder<Domain.Entities.Task> builder)
        {
            base.Configure(builder);

            builder.ToTable("Tasks");

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(x => x.Description)
                .HasMaxLength(300);

            builder.HasOne(x => x.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(x => x.ProjectId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User)
                .WithMany(p => p.Tasks)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
