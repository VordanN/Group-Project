namespace MoodleProject.Models
{
    public class Courses
    {
        public int Id { get; set; }
        public int Id_User_Teacher { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Holders>? Holders { get; set; }
    }
}
