using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using ts.api;
using ts.db;

namespace ts.api.Migrations
{
    [DbContext(typeof(AccountContext))]
    partial class AccountContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("api.Session", b =>
                {
                    b.Property<string>("SessionId");

                    b.Property<DateTime>("SessionEnd");

                    b.Property<DateTime>("SessionStart");

                    b.Property<long?>("UserUserId");

                    b.HasKey("SessionId");
                });

            modelBuilder.Entity("api.User", b =>
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

            modelBuilder.Entity("api.Session", b =>
                {
                    b.HasOne("api.User")
                        .WithMany()
                        .HasForeignKey("UserUserId");
                });
        }
    }
}
