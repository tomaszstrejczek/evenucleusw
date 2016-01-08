using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using ts.data;

namespace ts.data.Migrations
{
    [DbContext(typeof(AccountContext))]
    [Migration("20160108184124_Fix1")]
    partial class Fix1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ts.domain.ArchiveSession", b =>
                {
                    b.Property<long>("ArchiveSessionId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("SessionAccess");

                    b.Property<DateTime>("SessionEnd");

                    b.Property<string>("SessionId");

                    b.Property<DateTime>("SessionStart");

                    b.Property<long>("UserId");

                    b.HasKey("ArchiveSessionId");
                });

            modelBuilder.Entity("ts.domain.CacheEntry", b =>
                {
                    b.Property<string>("Key");

                    b.Property<DateTime>("CachedUntil");

                    b.Property<string>("Data");

                    b.HasKey("Key");
                });

            modelBuilder.Entity("ts.domain.Corporation", b =>
                {
                    b.Property<long>("CorporationId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("EveId");

                    b.Property<long>("KeyInfoId");

                    b.Property<string>("Name");

                    b.Property<long>("UserId");

                    b.Property<long?>("UserUserId");

                    b.HasKey("CorporationId");
                });

            modelBuilder.Entity("ts.domain.Job", b =>
                {
                    b.Property<int>("JobId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsManufacturing");

                    b.Property<bool>("JobCompleted");

                    b.Property<string>("JobDescription");

                    b.Property<DateTime>("JobEnd");

                    b.Property<string>("Owner");

                    b.Property<int>("PercentageOfCompletion");

                    b.Property<string>("Url");

                    b.Property<long>("UserId");

                    b.Property<long?>("UserUserId");

                    b.HasKey("JobId");
                });

            modelBuilder.Entity("ts.domain.KeyInfo", b =>
                {
                    b.Property<long>("KeyInfoId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("KeyId");

                    b.Property<long>("UserId");

                    b.Property<long?>("UserUserId");

                    b.Property<string>("VCode");

                    b.HasKey("KeyInfoId");
                });

            modelBuilder.Entity("ts.domain.Notification", b =>
                {
                    b.Property<long>("NotificationId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Error");

                    b.Property<string>("Message");

                    b.Property<string>("Message2");

                    b.Property<int>("Status");

                    b.Property<long>("UserId");

                    b.Property<long?>("UserUserId");

                    b.HasKey("NotificationId");
                });

            modelBuilder.Entity("ts.domain.Pilot", b =>
                {
                    b.Property<long>("PilotId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CurrentTrainingEnd");

                    b.Property<string>("CurrentTrainingNameAndLevel");

                    b.Property<long>("EveId");

                    b.Property<int>("FreeManufacturingJobsNofificationCount");

                    b.Property<int>("FreeResearchJobsNofificationCount");

                    b.Property<long>("KeyInfoId");

                    b.Property<int>("MaxManufacturingJobs");

                    b.Property<int>("MaxResearchJobs");

                    b.Property<string>("Name");

                    b.Property<bool>("TrainingActive");

                    b.Property<DateTime>("TrainingQueueEnd");

                    b.Property<long>("UserId");

                    b.Property<long?>("UserUserId");

                    b.HasKey("PilotId");
                });

            modelBuilder.Entity("ts.domain.Session", b =>
                {
                    b.Property<string>("SessionId")
                        .HasAnnotation("MaxLength", 96);

                    b.Property<DateTime>("SessionAccess");

                    b.Property<DateTime>("SessionEnd");

                    b.Property<DateTime>("SessionStart");

                    b.Property<long>("UserId");

                    b.HasKey("SessionId");
                });

            modelBuilder.Entity("ts.domain.Skill", b =>
                {
                    b.Property<int>("SkillId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Level");

                    b.Property<long>("PilotId");

                    b.Property<long?>("PilotPilotId");

                    b.Property<long?>("PilotPilotId1");

                    b.Property<string>("SkillName");

                    b.HasKey("SkillId");
                });

            modelBuilder.Entity("ts.domain.SkillInQueue", b =>
                {
                    b.Property<int>("SkillInQueueId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("LengthTicks");

                    b.Property<int>("Level");

                    b.Property<int>("Order");

                    b.Property<long>("PilotId");

                    b.Property<long?>("PilotPilotId");

                    b.Property<long?>("PilotPilotId1");

                    b.Property<string>("SkillName");

                    b.HasKey("SkillInQueueId");
                });

            modelBuilder.Entity("ts.domain.TypeNameEntry", b =>
                {
                    b.Property<long>("Key")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CachedUntil");

                    b.Property<string>("Data");

                    b.HasKey("Key");
                });

            modelBuilder.Entity("ts.domain.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("HashedPassword");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();
                });

            modelBuilder.Entity("ts.domain.Corporation", b =>
                {
                    b.HasOne("ts.domain.User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.HasOne("ts.domain.User")
                        .WithMany()
                        .HasForeignKey("UserUserId");
                });

            modelBuilder.Entity("ts.domain.Job", b =>
                {
                    b.HasOne("ts.domain.User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.HasOne("ts.domain.User")
                        .WithMany()
                        .HasForeignKey("UserUserId");
                });

            modelBuilder.Entity("ts.domain.KeyInfo", b =>
                {
                    b.HasOne("ts.domain.User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.HasOne("ts.domain.User")
                        .WithMany()
                        .HasForeignKey("UserUserId");
                });

            modelBuilder.Entity("ts.domain.Notification", b =>
                {
                    b.HasOne("ts.domain.User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.HasOne("ts.domain.User")
                        .WithMany()
                        .HasForeignKey("UserUserId");
                });

            modelBuilder.Entity("ts.domain.Pilot", b =>
                {
                    b.HasOne("ts.domain.User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.HasOne("ts.domain.User")
                        .WithMany()
                        .HasForeignKey("UserUserId");
                });

            modelBuilder.Entity("ts.domain.Session", b =>
                {
                    b.HasOne("ts.domain.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ts.domain.Skill", b =>
                {
                    b.HasOne("ts.domain.Pilot")
                        .WithMany()
                        .HasForeignKey("PilotId");

                    b.HasOne("ts.domain.Pilot")
                        .WithMany()
                        .HasForeignKey("PilotPilotId");

                    b.HasOne("ts.domain.Pilot")
                        .WithMany()
                        .HasForeignKey("PilotPilotId1");
                });

            modelBuilder.Entity("ts.domain.SkillInQueue", b =>
                {
                    b.HasOne("ts.domain.Pilot")
                        .WithMany()
                        .HasForeignKey("PilotId");

                    b.HasOne("ts.domain.Pilot")
                        .WithMany()
                        .HasForeignKey("PilotPilotId");

                    b.HasOne("ts.domain.Pilot")
                        .WithMany()
                        .HasForeignKey("PilotPilotId1");
                });
        }
    }
}
