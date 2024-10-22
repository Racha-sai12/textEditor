using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace TextEditor.Models
{
    public class Context : DbContext
    {
        public Context()
        {
            //SQLitePCL.Batteries_V2.Init();
            this.Database.EnsureCreated();
        }
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Chapter> Chapters { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "LocalDatabase1.db3");
            string s = FileSystem.AppDataDirectory;
            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasMany(e => e.Chapters)
                .WithOne(e => e.Project)
                .HasForeignKey(e => e.ProjetId)
                .IsRequired();
        }
    }
}
