﻿using System;
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

            modelBuilder.Entity<TrainingDay>()
                .HasMany(td => td.ExerciseStatuses)
                .WithOne(es => es.TrainingDay)
                .HasForeignKey(es => es.TrainingDayId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        //DB Sets below
        public DbSet<TrainingPlanExercise> TrainingPlanExercises { get; set; }
        public DbSet<TrainingPlan> TrainingPlans { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<TrainingDay> TrainingDays { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<ExerciseStatus> ExerciseStatuses { get; set; }
        public DbSet<ExerciseWeight> ExerciseWeights { get; set; }
    }
}

