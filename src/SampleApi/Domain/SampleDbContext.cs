using System;
using Microsoft.EntityFrameworkCore;

namespace SampleApi.Domain
{
    public class SampleDbContext : DbContext
    {
        public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<School> Schools { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<School>().HasMany(x => x.Students).WithOne(x => x.School).HasForeignKey(x => x.SchoolId);
        }

    }
}
