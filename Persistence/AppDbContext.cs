using System;
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
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserName = "genesys",
                    Password = "04980744f74f4ec36ad5a9d5fec8876f"
                });

            

            modelBuilder.Entity<User>().Property(b => b.AddTime).HasDefaultValueSql("getutcdate()");



        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("AppDbConnection"));
        }

        public DbSet<User> Users { get; set; }


    }
}