using System;
using Microsoft.EntityFrameworkCore;

namespace GymBro.Models.Data.EntityFramework
{
	public class ApplicationContext : DbContext
	{
        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=" + Constants.DatabasePath);
        }

        //DB Sets below
    }
}

