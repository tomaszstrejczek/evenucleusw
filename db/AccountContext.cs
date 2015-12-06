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
        public DbSet<Pilot> Pilots { get; set; }
        public DbSet<Corporation> Corporations { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<CacheEntry> CacheEntries { get; set; }

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
            modelBuilder.Entity<User>().HasMany<KeyInfo>().WithOne().HasForeignKey(k => k.UserId);
            modelBuilder.Entity<User>().HasMany<Pilot>().WithOne().HasForeignKey(k => k.UserId);
            modelBuilder.Entity<User>().HasMany<Corporation>().WithOne().HasForeignKey(k => k.UserId);
            modelBuilder.Entity<User>().HasMany<Job>().WithOne().HasForeignKey(k => k.UserId);
            modelBuilder.Entity<User>().HasMany<Notification>().WithOne().HasForeignKey(k => k.UserId);

            modelBuilder.Entity<Session>().Property(u => u.SessionId)
                .IsRequired()
                .HasMaxLength(96);
            modelBuilder.Entity<Session>().HasOne<User>();

            modelBuilder.Entity<Pilot>().HasMany<Skill>().WithOne().HasForeignKey(s => s.PilotId);
            modelBuilder.Entity<Pilot>()
                .Ignore(x => x.TrainedSkills)
                .Ignore(x => x.TrainingLengthDescription)
                .Ignore(x => x.TrainingWarning)
                .Ignore(x => x.TrainingNotActive)
                .Ignore(x => x.Url);

            modelBuilder.Entity<Corporation>()
                .Ignore(x => x.Url);

            modelBuilder.Entity<Skill>().HasOne<Pilot>();

            modelBuilder.Entity<Job>()
                .Ignore(x => x.DurationDescription);

            modelBuilder.Entity<CacheEntry>().HasKey(c => c.Key);
        }
    }
}
