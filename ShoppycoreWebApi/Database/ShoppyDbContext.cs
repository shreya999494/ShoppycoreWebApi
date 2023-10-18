using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ShoppycoreWebApi.Database;

public partial class ShoppyDbContext : DbContext
{
    public ShoppyDbContext()
    {
    }

    public ShoppyDbContext(DbContextOptions<ShoppyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-BMQUE5Q;Database=Shoppy_db;TrustServerCertificate=True;Persist Security Info=False;User ID=sb;Password=sa123;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK_Category");

            entity.Property(e => e.BrandId).HasColumnName("Brand_Id");
            entity.Property(e => e.Brands)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK_Products");

            entity.Property(e => e.ItemId).HasColumnName("Item_Id");
            entity.Property(e => e.BrandId).HasColumnName("Brand_Id");
            entity.Property(e => e.ItemType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Item_type");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Brand).WithMany(p => p.Items)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK_Items_Brand");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UId);

            entity.Property(e => e.UId).HasColumnName("U_Id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
