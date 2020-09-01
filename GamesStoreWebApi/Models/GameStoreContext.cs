using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesStoreWebApi.Models;

namespace GamesStoreWebApi.Models
{
    public class GameStoreContext : DbContext
    {
        public GameStoreContext(DbContextOptions<GameStoreContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<GamesStoreWebApi.Models.Products> Products { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserAddress> UserAddress { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here

            //Property Configurations
            modelBuilder.Entity<OrderDetail>()
                .HasKey(b => b.Id)
                .HasName("PrimaryKey_Id");
        }
    }
}
