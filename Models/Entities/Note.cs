namespace GymBro.Models.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public int ExerciseStatusId { get; set; }
        public ExerciseStatus ExerciseStatus { get; set; }

        public Note(string content)
        {
            Content = content;
        }

        public Note()
        {
        }
    }
}

