namespace GymBro.Models.Entities
{
    public class ExercisePosition
    {
        public int Id { get; set; }
        public Exercise Exercise { get; set; }
        public int ExerciseId { get; set; }
        public int Position { get; set; }

        public ExercisePosition() { }

        public ExercisePosition(Exercise exercise, int position) 
        {
            Exercise = exercise;
            Position = position;
        }
    }
}
