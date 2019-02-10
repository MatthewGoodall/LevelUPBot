using LevelUP.Core.Currency;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LevelUP.Resources.Database
{
    class SqliteDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder Options)
        {
            string DbLocation = Assembly.GetEntryAssembly().Location.Replace(@"bin\Debug\netcoreapp2.1", @"Data");
            Options.UseSqlite($"Data source={DbLocation}Database.sqlite");
            Console.WriteLine($"{DbLocation}Database.sqlite");
        }
    }
}
    