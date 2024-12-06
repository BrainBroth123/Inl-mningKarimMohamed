using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Slutuppgift_Karim_Mohamed.Models;

namespace Slutuppgift_Karim_Mohamed.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Slutuppgift_Karim_Mohamed;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasOne(p => p.Author).WithMany(a => a.Books).HasForeignKey(p => p.AuthorId);
        }
    }
}
