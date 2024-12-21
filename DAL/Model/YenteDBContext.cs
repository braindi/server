using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAL.Model
{
    public partial class YenteDBContext : DbContext
    {
        public YenteDBContext()
        {
        }

        public YenteDBContext(DbContextOptions<YenteDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attribute> Attributes { get; set; } = null!;
        public virtual DbSet<AttributeCategory> AttributeCategories { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=YenteDB;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attribute>(entity =>
            {
                entity.Property(e => e.AttributeId).HasColumnName("AttributeID");

                entity.Property(e => e.AttributeName).HasMaxLength(100);
            });

            modelBuilder.Entity<AttributeCategory>(entity =>
            {
                entity.HasKey(e => new { e.AttributeId, e.CategoryId })
                    .HasName("PK__Attribut__6019BA287B09936E");

                entity.Property(e => e.AttributeId).HasColumnName("AttributeID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.HasOne(d => d.Attribute)
                    .WithMany(p => p.AttributeCategories)
                    .HasForeignKey(d => d.AttributeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Attribute__Attri__2E1BDC42");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.AttributeCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Attribute__Categ__2F10007B");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName).HasMaxLength(100);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.AttributeId).HasColumnName("AttributeID");

                entity.HasMany(d => d.Attributes)
                    .WithMany(p => p.Questions)
                    .UsingEntity<Dictionary<string, object>>(
                        "QuestionAttribute",
                        l => l.HasOne<Attribute>().WithMany().HasForeignKey("AttributeId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__QuestionA__Attri__2B3F6F97"),
                        r => r.HasOne<Question>().WithMany().HasForeignKey("QuestionId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__QuestionA__Quest__2A4B4B5E"),
                        j =>
                        {
                            j.HasKey("QuestionId", "AttributeId").HasName("PK__Question__B1D8FD14E3621AD3");

                            j.ToTable("QuestionAttributes");

                            j.IndexerProperty<int>("QuestionId").HasColumnName("QuestionID");

                            j.IndexerProperty<int>("AttributeId").HasColumnName("AttributeID");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
