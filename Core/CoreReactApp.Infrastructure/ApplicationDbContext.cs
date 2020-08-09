using CoreReactApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreReactApp.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Blob> Blobs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var incidentsTable = modelBuilder.Entity<Incident>().ToTable("Incidents");

            incidentsTable
                .HasMany(x => x.Attachments)
                .WithOne(x => x.Incident)
                .HasForeignKey(x => x.IncidentId);

            incidentsTable
                .HasMany(x => x.Reviews)
                .WithOne(x => x.Incident)
                .HasForeignKey(x => x.IncidentId);

            modelBuilder.Entity<Category>().ToTable("Categories");

            var incCatRels = modelBuilder.Entity<IncidentCategory>().ToTable("IncidentCategoryRelationships");
            incCatRels
                .HasKey(x => new { x.IncidentId, x.CategoryId });
            incCatRels
                .HasOne(x => x.Incident)
                .WithMany(x => x.IncidentCategories)
                .HasForeignKey(x => x.IncidentId);
            incCatRels
                .HasOne(x => x.Category)
                .WithMany(x => x.IncidentCategories)
                .HasForeignKey(x => x.CategoryId);

            var attachmentsTable = modelBuilder.Entity<Attachment>().ToTable("Attachments");
            attachmentsTable
                .HasOne(x => x.Blob)
                .WithOne(x => x.Attachment)
                .HasForeignKey<Attachment>(x => x.BlobId)
                .OnDelete(DeleteBehavior.Cascade);

            var blobTable = modelBuilder.Entity<Blob>()
                .ToTable("Blobs");
            blobTable
                .HasOne(x => x.Attachment)
                .WithOne(x => x.Blob)
                .HasForeignKey<Blob>(x => x.AttachmentId)
                .OnDelete(DeleteBehavior.Cascade);

            var usersTable = modelBuilder.Entity<User>()
                .ToTable("Users");
            usersTable
                .HasIndex(i => i.Login)
                .IsUnique();

            var reviewsTable = modelBuilder.Entity<Review>().ToTable("Reviews");
            reviewsTable
                .HasMany(x => x.Attachments)
                .WithOne(x => x.Review)
                .HasForeignKey(x => x.ReviewId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
