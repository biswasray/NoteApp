using Microsoft.EntityFrameworkCore;
using System.Data;
using System;
using NoteApp.Entities;
using NoteApp.Configurations;

namespace NoteApp.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public static string ConnectionString = "Server=localhost;Port=3306;Database=poc;User=root;Password=human;AllowPublicKeyRetrieval=True;";
        public DbSet<Note> Notes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApplicationConfiguration());
        }
    }
}
