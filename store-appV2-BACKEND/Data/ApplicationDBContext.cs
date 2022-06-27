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

               
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
