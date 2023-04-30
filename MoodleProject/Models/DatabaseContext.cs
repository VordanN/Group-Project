using Microsoft.EntityFrameworkCore;
using MoodleProject.Models;

namespace MoodleProject
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        public MyDbContext()
        {}

        public DbSet<Users> Users { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<UsersTasks> UserTasks { get; set; }
        public DbSet<UsersCourses> UsersCourses { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Holders> Holders { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //}
    }
}
