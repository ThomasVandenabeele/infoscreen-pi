using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using InfoScreenPi.Models;

namespace InfoScreenPi.Migrations
{
    [DbContext(typeof(InfoScreenContext))]
    [Migration("20160621200307_Nieuwe2")]
    partial class Nieuwe2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20896");

            modelBuilder.Entity("InfoScreenPi.Models.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Background");

                    b.Property<string>("Content");

                    b.Property<int?>("SoortId");

                    b.Property<string>("Title");

                    b.HasKey("ItemId");

                    b.HasIndex("SoortId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("InfoScreenPi.Models.ItemKind", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.ToTable("ItemKinds");
                });

            modelBuilder.Entity("InfoScreenPi.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("InfoScreenPi.Models.Item", b =>
                {
                    b.HasOne("InfoScreenPi.Models.ItemKind")
                        .WithMany()
                        .HasForeignKey("SoortId");
                });
        }
    }
}
