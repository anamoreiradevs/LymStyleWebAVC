using LymStyleWebAPPAPI.Domain.DTO;
using LymStyleWebAPPAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LymStyleWebAPPAPI.Infra.Data.Repository.Contracts
{
    public class SQLServerContext : DbContext
    {
        public SQLServerContext(DbContextOptions<SQLServerContext> options) : base(options) 
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Seed
            modelBuilder.Entity<Account>()
            .HasData(
            new { Id = 1, Name= "Ana", Email = "adm@adm", Password = "adm" }
            );

            // Seed
            modelBuilder.Entity<Category>()
                .HasData(
                new { Id = 1, Name = "Woman" },
                new { Id = 2, Name = "Kids" },
                new { Id = 3, Name = "Man" },
                new { Id = 4, Name = "Teenager" }
                );
        }
        #region DBSets<Tables>

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Account> Users{ get; set; }
        #endregion
    }
}
