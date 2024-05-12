using System;
using GymBro.Models.Entities;
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
        public DbSet<TrainingScheduleExercises> TrainingScheduleExercises { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<ExerciseStatus> ExerciseStatuses { get; set; }
        public DbSet<ExerciseWeight> ExerciseWeights { get; set; }
    }
}

