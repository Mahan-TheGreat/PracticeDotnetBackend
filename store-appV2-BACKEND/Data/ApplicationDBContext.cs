using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using store_appV2_BACKEND.Models;

namespace store_appV2_BACKEND.Data
{
    public partial class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext()
        {
        }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dispencary> Dispencaries { get; set; } = null!;
        public virtual DbSet<Inventory> Inventories { get; set; } = null!;
        public virtual DbSet<TxnSale> TxnSales { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UsersCred> UsersCreds { get; set; } = null!;
        public object UsersCred { get; internal set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-3G90KEK\\SQLEXPRESS;Initial Catalog=StoreApp;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dispencary>(entity =>
            {
                entity.ToTable("Dispencary");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DispencaryCode)
                    .HasMaxLength(10)
                    .HasColumnName("dispencaryCode");

                entity.Property(e => e.DispencaryLocation)
                    .HasMaxLength(50)
                    .HasColumnName("dispencaryLocation");

                entity.Property(e => e.DispencaryName).HasColumnName("dispencaryName");

                entity.Property(e => e.IsActive).HasColumnName("isActive");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("Inventory");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.ItemDetails).HasColumnName("itemDetails");

                entity.Property(e => e.ItemName)
                    .HasMaxLength(100)
                    .HasColumnName("itemName");

                entity.Property(e => e.ItemPrice).HasColumnName("itemPrice");

                entity.Property(e => e.Quantity).HasColumnName("quantity");
            });

            modelBuilder.Entity<TxnSale>(entity =>
            {
                entity.ToTable("TXN_Sales");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ItemId).HasColumnName("itemId");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.TotalPrice).HasColumnName("totalPrice");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.TxnSales)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TXN_Sales__itemI__5CD6CB2B");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Contact)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("contact");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("firstName");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("lastName");

                entity.Property(e => e.Location)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("location");
            });

            modelBuilder.Entity<UsersCred>(entity =>
            {
                entity.ToTable("UsersCred");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.Location)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.Password)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("userName");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
