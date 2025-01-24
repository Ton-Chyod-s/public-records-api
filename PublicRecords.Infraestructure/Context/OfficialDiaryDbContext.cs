using PublicRecords.Domain.Entities.BaseEntity;
using PublicRecords.Domain.Entities.OfficialStateDiary;
using PublicRecords.Domain.Entities.Person;
using PublicRecords.Domain.Entities.Session;
using PublicRecords.Domain.Entities.Token;
using PublicRecords.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace PublicRecords.Infraestructure.Context
{
    public sealed class OfficialDiaryDbContext(DbContextOptions<OfficialDiaryDbContext> options) : DbContext(options)
    {

        #region [DbSet]
        public DbSet<OfficialDiaries> OfficialDiaries { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<AuthToken> AuthToken { get; set; }
        
        #endregion

        #region [Config Official Diary]
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OfficialDiaryDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                if (entry.State == EntityState.Modified)
                    entry.Entity.UpdateAt = DateTime.UtcNow;
                else if (entry.State == EntityState.Added)
                    entry.Entity.CreateAt ??= DateTime.UtcNow;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        #endregion
    }
}
