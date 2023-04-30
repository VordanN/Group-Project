using Microsoft.Build.Framework;
using System.Data;
using System.Security.Principal;

namespace MoodleProject.Models
{
    public class Holders
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Id_Course { get; set; }
        public int CourseId { get; set; }
        public virtual Courses? Course { get; set; }
        public virtual ICollection<Tasks>? Tasks { get; set; }

    }
}
