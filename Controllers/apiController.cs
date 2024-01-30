using Humanizer.Localisation.TimeToClockNotation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MSIT155Site.Models;
using MSIT155Site.Models.DTO;
using System.Text;

namespace MSIT155Site.Controllers
{
    public class apiController : Controller
    {
        private readonly MyDBContext _context;
        public apiController(MyDBContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
           Thread.Sleep(5000);
            return Content("Hello Content!!你好", "text/plain", Encoding.UTF8);
        }
        //public IActionResult Register(string name, int age)
        public IActionResult Register(UserDTO _user)
        {
            if (string.IsNullOrEmpty(_user.Name))
            {
                _user.Name = "guest";
            }
            return Content($"Hello {_user.Name}，您已 {_user.Age} 歲了!電子郵件:{_user.Email}", "text/plain", Encoding.UTF8);
        }
        public IActionResult Cities()
        {
            var cities = _context.Addresses.Select(x => x.City).Distinct();
            return Json(cities);
        }
        public IActionResult avator(int id = 1)
        {
            Member? member = _context.Members.Find(id);
            if (member != null)
            {
                byte[] img = member.FileData;
                if (img != null)
                {
                    return File(img, "image/jpeg");
                }

            }
            return NotFound();
        }

        public IActionResult First()
        {
            return View();
        }
    }
}
