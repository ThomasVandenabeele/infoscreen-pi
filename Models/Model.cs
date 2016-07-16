using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace InfoScreenPi.Models
{
    public class InfoScreenContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Background> Backgrounds { get; set; }
        public DbSet<ItemKind> ItemKinds { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./InfoScreenDB.db");
        }
    }

    public class Item
    {
        public int ItemId { get; set; }
        public ItemKind Soort { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Background Background { get; set; }
        public Boolean Active { get; set; }
        public Boolean Archieved { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
    }

    public class ItemKind
    {
        public int Id {get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
    }

    public class Background
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }

}