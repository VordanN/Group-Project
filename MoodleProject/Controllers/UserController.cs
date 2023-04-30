using Microsoft.AspNetCore.Mvc;
using MoodleProject.Models;
using System.Data.Entity.Infrastructure;
using System.Security.Cryptography;

namespace MoodleProject.Controllers
{
    public class UserController : Controller
    {
        private readonly MyDbContext _context;

        public UserController(MyDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Login(IFormCollection collection)
        {
            string email = collection["email"].ToString();
            string password = collection["password"].ToString();

            if (_context.Users.Any(u => u.Email == email && u.Password == Utils.Utils.ConvertStringtoMD5(password)))
            {
                Response.Cookies.Append("UserId", _context.Users.FirstOrDefault(u => u.Email == email).Id.ToString());

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("UserId");
            return RedirectToAction("Login", "User");
        }

        public IActionResult Profile()
        {
            var User = _context.Users.Find(int.Parse(Request.Cookies["UserId"]));

            return View(User);
        }

        public IActionResult Edit()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            var users = _context.Users.Find(int.Parse(Request.Cookies["UserId"]));
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,LastName,Email,Password,permisson_level")] Users users)
        {
            if (id != users.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    users.Password = Utils.Utils.ConvertStringtoMD5(users.Password);
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }
        private bool UsersExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
