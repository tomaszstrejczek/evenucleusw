using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using ts.shared;

namespace ts.db
{
    public class AccountContext: DbContext
    {
        private readonly IMyConfiguration _configuration;
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<ArchiveSession> ArchiveSessions { get; set; }
        public DbSet<KeyInfo> KeyInfos { get; set; }

        public AccountContext(IMyConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_configuration == null)
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=evenucleusw;Integrated Security=True");
            else if (_configuration.UseSql)
                optionsBuilder.UseSqlServer(_configuration.ConnectionString);
            else
                optionsBuilder.UseInMemoryDatabase();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>().Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);
            modelBuilder.Entity<Session>().Property(u => u.SessionId)
                .IsRequired()
                .HasMaxLength(96);

            modelBuilder.Entity<Session>().HasOne<User>();

            modelBuilder.Entity<User>().HasMany<KeyInfo>();
        }
    }
}
