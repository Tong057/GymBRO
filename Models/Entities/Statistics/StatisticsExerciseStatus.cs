using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymBro.Models.Entities.Statistics
{
    public class StatisticsExerciseStatus
    {
        public DateOnly Date { get; private set; }
        public double WeightedAverageWeight { get; private set; }

        public StatisticsExerciseStatus(ExerciseStatus exerciseStatus, DateOnly date)
        {
            Date = date;
            
        }
    }
}
