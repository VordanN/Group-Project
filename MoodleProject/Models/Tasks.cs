namespace MoodleProject.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? End_Date { get; set; }
        public string? File_Path { get; set; }
        public string? Url { get; set; }
        public int? Holder_id { get; set; }
        public int? HoldersId { get; set; }

    }
}
