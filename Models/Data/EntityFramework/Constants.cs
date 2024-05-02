
namespace GymBro.Models.Data.EntityFramework
{
    public class Constants
    {
        public const string DatabaseFileName = "gymbro.db3";
        public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFileName);
    }
}
