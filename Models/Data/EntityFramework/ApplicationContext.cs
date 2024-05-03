using System;
using GymBro.Models.Data.EntityFramework.Models;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }



        //DB Sets below
        public DbSet<ScheduleDay> ScheduleDays { get; set; }
        public DbSet<TrainingSchedule> TrainingSchedules { get; set; }
    }
}

