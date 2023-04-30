using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using MoodleProject.Models;
using System.Data.Entity;
using System.Security.Policy;
using static NuGet.Packaging.PackagingConstants;


namespace MoodleProject.Controllers
{
    public class CourseController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CourseController(MyDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int? id)
        {
            var courses = _context.Courses.FirstOrDefault(c => c.Id == id);

            return View(courses);
        }

        public IActionResult View(int? id)
        {
            var courses = _context.Courses.FirstOrDefault(c => c.Id == id);

            return View("View", courses);
        }
        [HttpPost]
        public async Task<IActionResult> AddTask(IFormCollection collection)
        {
            if (collection.Files.First() != null && collection.Files.First().Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(collection.Files.First().FileName);
                var filePath = Path.Combine(_env.WebRootPath, "uploads", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await collection.Files.First().CopyToAsync(fileStream);
                }
                var task = new Tasks()
                {
                    Name = collection["taskName"],
                    Description = collection["taskDescription"],
                    End_Date = collection["taskEndDate"],
                    File_Path = filePath,
                    Url = collection["taskUrl"],
                    Holder_id = int.Parse(collection["holderId"]),                    
                };
                _context.Tasks.Add(task);
                _context.SaveChanges();
                return View("Index",_context.Holders.FirstOrDefault(x => x.Id == task.Holder_id).Course);
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddHolder(IFormCollection collection)
        {
            var holder = new Holders()
            {
                Name = collection["holderNameInput"],
                Id_Course = int.Parse(collection["course_Id"]),
                CourseId = int.Parse(collection["course_Id"]),
                Tasks = new List<Tasks>()
            };
            _context.Holders.Add(holder);
            _context.SaveChanges();
            return View("Index", _context.Courses.FirstOrDefault(c => c.Id == holder.Id_Course));
        }

        public ActionResult Courses()
        {
            Users currentUser= _context.Users.Find(int.Parse(Request.Cookies["UserId"]));
            var courses = _context.Courses
            .Where(c => c.Id_User_Teacher == currentUser.Id)
            .ToList();

            return View(courses);
        }
    }
}
