using Application.Interfaces.ServicesInterfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

namespace Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenService _tokenService;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IDateTimeService dateTime,
            IHttpContextAccessor httpContextAccessor
,
            ITokenService tokenService) : base(options)
        {
            _dateTime = dateTime;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _httpContextAccessor = httpContextAccessor;
            _tokenService = tokenService;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Domain.Entities.Task> Tasks { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                HttpContext? httpContext = _httpContextAccessor.HttpContext;
                
                switch (entry.State)
                {
                    case EntityState.Added:
                        if (httpContext is not null && !httpContext.Request.Headers.Authorization.IsNullOrEmpty())
                        {
                            Guid userId = await _tokenService.GetUserIdFromJwt(httpContext) ?? throw new UnauthorizedAccessException();
                            entry.Entity.CreatedBy = userId;
                        }
                        entry.Entity.CreatedAt = _dateTime.NowUTC;
                        break;
                    case EntityState.Modified:
                        if (httpContext is not null && !httpContext.Request.Headers.Authorization.IsNullOrEmpty())
                        {
                            Guid userId = await _tokenService.GetUserIdFromJwt(httpContext) ?? throw new UnauthorizedAccessException();
                            entry.Entity.LastModifiedBy = userId;
                        }
                        entry.Entity.LastModifiedAt = _dateTime.NowUTC;
                        break;
                } 
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
