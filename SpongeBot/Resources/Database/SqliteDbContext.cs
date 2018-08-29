using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace SpongeBot.Resources.Database
{
    public class SqliteDbContext : DbContext
    {
        public DbSet<Clam> Clams { get; set; }
        public DbSet<Dubloon> Dubloons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder Options)
        {
            string DbLocation = Assembly.GetEntryAssembly().Location.Replace(@"bin\Debug\netcoreapp2.0", @"Data\");
            //Options.UseSqlite("Data Source=" + DbLocation);
            Options.UseSqlite($"Data Source=Database.sqlite");
            return;
        }
    }
}
