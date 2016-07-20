using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using InfoScreenPi.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InfoScreenPi.Infrastructure
{
    public class InfoScreenContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RssFeed> RssFeeds { get; set; }
        public DbSet<Background> Backgrounds { get; set; }
        public DbSet<ItemKind> ItemKinds { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Error> Errors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./InfoScreenDB.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = entity.DisplayName();
            }

            // User
            modelBuilder.Entity<User>().Property(u => u.Username).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<User>().Property(u => u.HashedPassword).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<User>().Property(u => u.Salt).IsRequired().HasMaxLength(200);

            // UserRole
            modelBuilder.Entity<UserRole>().Property(ur => ur.UserId).IsRequired();
            modelBuilder.Entity<UserRole>().Property(ur => ur.RoleId).IsRequired();

            // Role
            modelBuilder.Entity<Role>().Property(r => r.Name).IsRequired().HasMaxLength(50);
        }

    }

}