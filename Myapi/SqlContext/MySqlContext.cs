using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Myapi.Models;

namespace Myapi.SqlContext
{
    public class MySqlContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Application> Applications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                   => optionsBuilder.UseMySql(@"Server=bdm275410299.my3w.com;database=bdm275410299_db;uid=bdm275410299;pwd=tanwenbin");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
