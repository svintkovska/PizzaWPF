using DAL.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Data
{
    public class EFAppContext : DbContext
    {
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ProductImageEntity> ProductImages { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<BasketEntity> Baskets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql("Server=localhost;Port=5432;Database=teambv121;User Id=postgres;Password=123456;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BasketEntity>(basket => {
                basket.HasKey(b => new { b.UserId, b.ProductId });
            });
        }
    }
}
