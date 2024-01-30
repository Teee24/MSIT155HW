using Microsoft.AspNetCore.Mvc;
using MSIT155Site.Models;
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
        [HttpPost]
        public IActionResult Index()
        {
            return Content("<h2>hello 你好</h2>", "text/html", Encoding.UTF8);
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
    }
}
