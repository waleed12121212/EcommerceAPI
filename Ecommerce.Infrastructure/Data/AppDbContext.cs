﻿using Ecommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Ecommerce.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetails>()
                .HasKey(x => new { x.Id , x.ProductId , x.OrderId });

            modelBuilder.Entity<Categories>().HasData(
            new Categories { Id = 1 , Name = "Electronics" , Description = "Devices and gadgets" } ,
            new Categories { Id = 2 , Name = "Books" , Description = "Books and literature" } ,
            new Categories { Id = 3 , Name = "Clothes" , Description = "Devices and gadgets" }
            );
            modelBuilder.Entity<LocalUser>().HasData(
            new LocalUser { Id = 1 , Name = "Ahmed Haggag" , UserName = "Haggag281" , Password = "password123" , Role = "Admin" } ,
            new LocalUser { Id = 2 , Name = "waleed dweikat" , UserName = "waleed67" , Password = "password1ee3" , Role = "User" }
            );

            modelBuilder.Entity<Products>().HasData(
            new Products { Id = 1 , Name = "Smartphone" , Price = 299.99m , Image = "smartphone.jpg" , Category_Id = 1 } ,
            new Products { Id = 2 , Name = "Laptop" , Price = 799.99m , Image = "laptop.jpg" , Category_Id = 1 } ,
            new Products { Id = 3 , Name = "Novel" , Price = 19.99m , Image = "novel.jpg" , Category_Id = 2 } ,
            new Products { Id = 4 , Name = "T-Shirt" , Price = 9.99m , Image = "tshirt.jpg" , Category_Id = 3 } ,
            new Products { Id = 5 , Name = "Jeans" , Price = 49.99m , Image = "jeans.jpg" , Category_Id = 3 }
            );
            modelBuilder.Entity<Orders>().HasData(
            new Orders { Id = 1 , OrderStatus = "Pending" , OrderDate = new DateTime(2023 , 12 , 11) , LocalUserId = 1 } ,
            new Orders { Id = 2 , OrderStatus = "Completed" , OrderDate = new DateTime(2023 , 12 , 12) , LocalUserId = 2 } ,
            new Orders { Id = 3 , OrderStatus = "Shipped" , OrderDate = new DateTime(2023 , 12 , 13) , LocalUserId = 1 }
            );

            modelBuilder.Entity<OrderDetails>().HasData(
            new OrderDetails { Id = 1 , OrderId = 1 , ProductId = 1 , price = 299.99m , quantity = 1 } ,
            new OrderDetails { Id = 2 , OrderId = 1 , ProductId = 4 , price = 299.99m , quantity = 2 } ,
            new OrderDetails { Id = 3 , OrderId = 2 , ProductId = 3 , price = 299.99m , quantity = 1 } ,
            new OrderDetails { Id = 4 , OrderId = 3 , ProductId = 2 , price = 299.99m , quantity = 1 } ,
            new OrderDetails { Id = 5 , OrderId = 3 , ProductId = 5 , price = 299.99m , quantity = 1 }
            );
        }
        public DbSet<Products> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<LocalUser> LocalUsers { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
    }
}