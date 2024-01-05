﻿using Core.Entities.Concrete;
//using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Entities.Dtos;

namespace DataAccess.Concrete.EntityFramework
{
    public class DataBaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=OSKYY;Database=Market;Trusted_Connection=True;TrustServerCertificate=True");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Pos> Pos { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerMovement> CustomerMovements { get; set; }
        public DbSet<WholeSaler> WholeSalers { get; set; }
        public DbSet<WholeSalerMovement> WholeSalerMovements { get; set; }
    }
}
