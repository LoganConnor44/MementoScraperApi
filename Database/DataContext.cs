using Microsoft.EntityFrameworkCore;
using MementoScraperApi.Models;

namespace MementoScraperApi.Database
{
    public class DataContext : DbContext
    {
        public DbSet<Memento> Mementos { get; set; }
        public DbSet<Memory> Memories { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
        : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.HasDefaultSchema("MementoScraperDatabase");

            modelBuilder.Entity<Memento>()
                .HasKey(x => x.Id)
                .HasName("MEMENTO_PK");
            modelBuilder.Entity<Memento>()
                .Property(x => x.Type)
                .HasColumnName("TYPE")
                .IsRequired();
            modelBuilder.Entity<Memento>()
                .Property(x => x.Comment)
                .HasColumnName("COMMENT")
                .ValueGeneratedNever();
            modelBuilder.Entity<Memento>()
                .Property(x => x.Owner)
                .HasColumnName("OWNER")
                .IsRequired();
            modelBuilder.Entity<Memento>()
                .Property(x => x.Phrase)
                .HasColumnName("PHRASE")
                .IsRequired();
            modelBuilder.Entity<Memento>()
                .Property(x => x.Creation)
                .HasColumnName("CREATION")
                .IsRequired()
                .HasColumnType("datetime");

            modelBuilder.Entity<Memory>()
                .HasKey(x => x.Id)
                .HasName("MEMORY_PK");
            modelBuilder.Entity<Memory>()
                .Property(x => x.MediaURL)
                .HasColumnName("MEDIA_URL");
            modelBuilder.Entity<Memory>()
                .Property(x => x.MediaURLHttps)
                .HasColumnName("MEDIA_URL_HTTPS");
            modelBuilder.Entity<Memory>()
                .Property(x => x.Url)
                .HasColumnName("URL");
            modelBuilder.Entity<Memory>()
                .Property(x => x.DisplayURL)
                .HasColumnName("DISPLAY_URL");
            modelBuilder.Entity<Memory>()
                .Property(x => x.ExpandedURL)
                .HasColumnName("EXPANDED_URL");
            modelBuilder.Entity<Memory>()
                .Property(x => x.MediaType)
                .HasColumnName("MEDIA_TYPE")
                .IsRequired();
            modelBuilder.Entity<Memory>()
                .Property(x => x.Creation)
                .HasColumnName("CREATION")
                .IsRequired()
                .HasColumnType("datetime");
            modelBuilder.Entity<Memory>()
                .HasOne(x => x.Memento)
                .WithMany(x => x.Memories)
                .HasForeignKey(x => x.MementoId);
        }
    }
}