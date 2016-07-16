using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using InfoScreenPi.Models;

namespace InfoScreenPi.Migrations
{
    [DbContext(typeof(InfoScreenContext))]
    [Migration("20160709192527_DatabaseCreate")]
    partial class DatabaseCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20896");

            modelBuilder.Entity("InfoScreenPi.Models.Background", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("Backgrounds");
                });

            modelBuilder.Entity("InfoScreenPi.Models.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<bool>("Archieved");

                    b.Property<int?>("BackgroundId");

                    b.Property<string>("Content");

                    b.Property<int?>("SoortId");

                    b.Property<string>("Title");

                    b.HasKey("ItemId");

                    b.HasIndex("BackgroundId");

                    b.HasIndex("SoortId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("InfoScreenPi.Models.ItemKind", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Source");

                    b.HasKey("Id");

                    b.ToTable("ItemKinds");
                });

            modelBuilder.Entity("InfoScreenPi.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("LastLogin");

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("InfoScreenPi.Models.Item", b =>
                {
                    b.HasOne("InfoScreenPi.Models.Background")
                        .WithMany()
                        .HasForeignKey("BackgroundId");

                    b.HasOne("InfoScreenPi.Models.ItemKind")
                        .WithMany()
                        .HasForeignKey("SoortId");
                });
        }
    }
}
