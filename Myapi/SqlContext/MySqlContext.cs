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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                   => optionsBuilder.UseMySql(@"Server=localhost;database=ef;uid=root;pwd=root");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
