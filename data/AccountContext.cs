using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

using ts.domain;

namespace ts.data
{
    public class AccountContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<ArchiveSession> ArchiveSessions { get; set; }
        public DbSet<KeyInfo> KeyInfos { get; set; }
        public DbSet<Pilot> Pilots { get; set; }
        public DbSet<Corporation> Corporations { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SkillInQueue> SkillsInQueue { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<CacheEntry> CacheEntries { get; set; }
        public DbSet<TypeNameEntry> TypeNameEntries { get; set; }

        public AccountContext()
        {
        }

        internal class Config
        {
            internal Config()
            {
                if (System.Environment.MachineName.ToLower() == "vir")
                    ConnectionString = Local;
                else
                    ConnectionString = Azure;
            }

            internal string ConnectionString { get; set; }

            private string Local => "Server=vir;Database=myDataBase;Trusted_Connection=True;";
            private string Azure => "Server=tcp:evenucleusw.database.windows.net,1433;Database=evenucleusw;User ID=tomek@evenucleusw;Password=Traktor12;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }

        static private Config _config = new Config();

        static public bool UseInMemory = false;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (UseInMemory)
            {
                optionsBuilder.UseInMemoryDatabase();
            }
            else
                optionsBuilder.UseSqlServer(_config.ConnectionString);
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
            modelBuilder.Entity<Pilot>().HasMany<SkillInQueue>().WithOne().HasForeignKey(s => s.PilotId);
            modelBuilder.Entity<Pilot>()
                .Ignore(x => x.TrainingLengthDescription)
                .Ignore(x => x.TrainingWarning)
                .Ignore(x => x.TrainingNotActive)
                .Ignore(x => x.Url);

            modelBuilder.Entity<Corporation>()
                .Ignore(x => x.Url);

            modelBuilder.Entity<Skill>().HasOne<Pilot>();
            modelBuilder.Entity<SkillInQueue>().HasOne<Pilot>();
            modelBuilder.Entity<SkillInQueue>().Ignore(c => c.Length);

            modelBuilder.Entity<Job>()
                .Ignore(x => x.DurationDescription);

            modelBuilder.Entity<CacheEntry>().HasKey(c => c.Key);

            modelBuilder.Entity<TypeNameEntry>().HasKey(c => c.Key);
        }
    }
}
