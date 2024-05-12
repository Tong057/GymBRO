using System;
namespace GymBro.Models.Entities
{
    public class Training
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public ICollection<ExerciseStatus> ExerciseStatuses { get; set; } = new List<ExerciseStatus>();

        public int TrainingScheduleId { get; set; }
        public TrainingSchedule TrainingSchedule { get; set; }

        public Training(TrainingSchedule trainingSchedule)
        {
            TrainingScheduleId = trainingSchedule.Id;
            TrainingSchedule = trainingSchedule;
        }

        public Training()
        {
        }
    }
}

