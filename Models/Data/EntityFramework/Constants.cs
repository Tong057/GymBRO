
namespace GymBro.Models.Data.EntityFramework
{
    public class Constants
    {
        public const string DatabaseFileName = "gymbrodb.db3";
        public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFileName);
    }
}
