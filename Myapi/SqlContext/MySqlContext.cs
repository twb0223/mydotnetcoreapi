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
<<<<<<< HEAD
                   => optionsBuilder.UseMySql(@"Server=bdm275410299.my3w.com;database=bdm275410299_db;uid=bdm275410299;pwd=tanwenbin");

=======
                   => optionsBuilder.UseMySql(@"Server=localhost;database=ef;uid=root;pwd=root");
>>>>>>> c40a4f250b90192748abb914408bd62b5dcc0d61
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
