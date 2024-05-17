using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymBro.Models.Entities.Statistics
{
    public class StatisticsExerciseStatus
    {
        public DateTime Date { get; private set; }
        public Exercise Exercise { get; private set; }
        public double WeightedAverageWeight { get; private set; }

        public StatisticsExerciseStatus(ExerciseStatus exerciseStatus, DateTime date)
        {
            Date = date;
            Exercise = exerciseStatus.Exercise;
            WeightedAverageWeight = CalculateWeightedAverageWeight(exerciseStatus.ExerciseSets);
        }

        public StatisticsExerciseStatus(DateTime date, double weightedAverageWeight)
        {
            Date = date;
            WeightedAverageWeight = weightedAverageWeight;
        }

        private double CalculateWeightedAverageWeight(ICollection<ExerciseSet> exerciseSets)
        {
            if (exerciseSets.Count == 0 && exerciseSets.Any(set => set.Weight == null || set.Repeats == null))
            {
                return 0; 
            }

            double totalWeightTimesRepeats = 0;
            int totalRepeats = 0;

            foreach (var set in exerciseSets)
            {
                totalWeightTimesRepeats += (double)(set.Weight * set.Repeats);
                totalRepeats += (int)set.Repeats;
            }

            return totalWeightTimesRepeats / totalRepeats;
        }
    }
}
