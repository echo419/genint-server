﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Models;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using Core.ViewModels;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection.Metadata;


namespace Persistence
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(IConfiguration config)
        {
            _config = config;
        }

        public IConfiguration _config { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserName = "genesys",
                    PasswordHash = "04980744f74f4ec36ad5a9d5fec8876f"
                });

            

            modelBuilder.Entity<User>().Property(b => b.AddTime).HasDefaultValueSql("getutcdate()");
            modelBuilder.Entity<AppContentElement>().Property(b => b.AddTime).HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<AppContentElement>()
            .HasOne(s => s.Parent)
            .WithMany(m => m.Children)
            .HasForeignKey(e => e.ParentId);


            base.OnModelCreating(modelBuilder);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("AppDbConnection"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<AppContentElement> AppContentElements { get; set; }


    }
}