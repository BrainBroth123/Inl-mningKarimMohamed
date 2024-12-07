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
        public DbSet<AuthorRegistration> AuthorRegistrations { get; set; }
        public DbSet<LoanRegistration> LoanRegistrations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Slutuppgift_Karim_Mohamed;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasMany(p => p.Registrations)
                .WithOne(a => a.Book)
                .HasForeignKey(p => p.BookId)
                .HasForeignKey(p => p.AuthorId);

            modelBuilder.Entity<Author>()
                .HasMany(p => p.Registrations)
                .WithOne(a => a.Author)
                .HasForeignKey(p => p.AuthorId)
                .HasForeignKey(p => p.BookId);

            modelBuilder.Entity<LoanRegistration>()
                .HasOne(p => p.Book);
        }
    }
}
