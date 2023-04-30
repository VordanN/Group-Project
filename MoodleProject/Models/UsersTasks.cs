namespace MoodleProject.Models
{
    public class UsersTasks
    {
        public int Id { get; set; }
        public int Id_User { get; set; }
        public int Id_Task { get; set; }
        public string? File_Path { get; set; }
        public int Mark { get; set; }
    }
}
