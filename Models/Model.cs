using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace InfoScreenPi.Models
{
    public class InfoScreenContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }
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
        public string Background { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class ItemKind
    {
        public int Id {get; set; }
        public string Description { get; set; } 
    }

}