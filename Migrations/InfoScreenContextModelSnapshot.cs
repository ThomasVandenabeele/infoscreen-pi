using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using InfoScreenPi.Infrastructure;

namespace InfoScreenPi.Migrations
{
    [DbContext(typeof(InfoScreenContext))]
    partial class InfoScreenContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("InfoScreenPi.Entities.Background", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("Background");
                });

            modelBuilder.Entity("InfoScreenPi.Entities.Error", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Message");

                    b.Property<string>("StackTrace");

                    b.HasKey("Id");

                    b.ToTable("Error");
                });

            modelBuilder.Entity("InfoScreenPi.Entities.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<bool>("Archieved");

                    b.Property<int?>("BackgroundId");

                    b.Property<string>("Content");

                    b.Property<int?>("RssFeedId");

                    b.Property<int?>("SoortId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("BackgroundId");

                    b.HasIndex("RssFeedId");

                    b.HasIndex("SoortId");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("InfoScreenPi.Entities.ItemKind", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Source");

                    b.HasKey("Id");

                    b.ToTable("ItemKind");
                });

            modelBuilder.Entity("InfoScreenPi.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("InfoScreenPi.Entities.RssFeed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("RssFeed");
                });

            modelBuilder.Entity("InfoScreenPi.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 200);

                    b.Property<string>("FirstName");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 200);

                    b.Property<bool>("IsLocked");

                    b.Property<DateTime>("LastLogin");

                    b.Property<string>("LastName");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 200);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("InfoScreenPi.Entities.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("RoleId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("InfoScreenPi.Entities.Item", b =>
                {
                    b.HasOne("InfoScreenPi.Entities.Background", "Background")
                        .WithMany()
                        .HasForeignKey("BackgroundId");

                    b.HasOne("InfoScreenPi.Entities.RssFeed", "RssFeed")
                        .WithMany()
                        .HasForeignKey("RssFeedId");

                    b.HasOne("InfoScreenPi.Entities.ItemKind", "Soort")
                        .WithMany()
                        .HasForeignKey("SoortId");
                });

            modelBuilder.Entity("InfoScreenPi.Entities.UserRole", b =>
                {
                    b.HasOne("InfoScreenPi.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InfoScreenPi.Entities.User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
