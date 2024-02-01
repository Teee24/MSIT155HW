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
        private readonly IWebHostEnvironment _environment;
        public apiController(MyDBContext context,IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        
        public IActionResult Index()
        {
           Thread.Sleep(5000);
            return Content("Hello Content!!你好", "text/plain", Encoding.UTF8);
        }
        //public IActionResult Register(string name, int age)
        [HttpPost]
        public IActionResult Register(UserDTO _user)
        {
            if (string.IsNullOrEmpty(_user.Name))
            {
                _user.Name = "guest";
            }
            //讓檔名不為空直
            string filename = "empty.jpg";
            if (_user.Avator?.FileName != null)
            {
                filename = _user.Avator.FileName;
            }
            //get 實際path
            string uploadPath = Path.Combine(_environment.WebRootPath,"uploads", filename);
            //save img into db
            using (var fileStream = new FileStream(uploadPath, FileMode.Create))
            {
                _user.Avator?.CopyTo(fileStream);
            }

            //return Content($"Hello {_user.Name}，您已 {_user.Age} 歲了!電子郵件:{_user.Email}", "text/plain", Encoding.UTF8);
            //return Content($"{_user.Avator?.FileName} - {_user.Avator?.Length} - {_user.Avator?.ContentType}");
            return Content(uploadPath);
        }
        public IActionResult Cities()
        {
            var cities = _context.Addresses.Select(x => x.City).Distinct();
            return Json(cities);
        }
        public IActionResult District(string city)
        {
            var district = _context.Addresses.Where(m=>m.City==city).Select(m=>m.SiteId.Substring(3,3)).Distinct();
            return Json(district);
        }
        public IActionResult Road(string district)
        {
            var street = _context.Addresses.Where(m => m.SiteId.Contains( district)).Select(m => m.Road);

            return Json(street);
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

        public IActionResult CheckAccountAction(UserDTO _user)
        {

            bool result = _context.Members.Any( m=>m.Name==_user.Name) ;
            
            return Content(result.ToString());
        }
        [HttpPost]
        public IActionResult Spots([FromBody]searchDTO _search)
        {
            //根據分類取景點
            var spots = _search.categoryId == 0 ? _context.SpotImagesSpots:_context.SpotImagesSpots
                .Where(m=>m.CategoryId==_search.categoryId);

            //search for keywords
            spots = spots.Where(m=>m.SpotTitle.Contains(_search.keyword)|| m.SpotDescription.Contains(_search.keyword));

            //排序
            switch (_search.sortBy)
            {
                case "SpotTitle":
                    spots = _search.sortType == "asc" ?spots.OrderBy(m=>m.SpotTitle) : spots.OrderByDescending(m => m.SpotTitle);
                    break;
                case "SpotDescription":
                    spots = _search.sortType == "asc" ? spots.OrderBy(m => m.SpotDescription) : spots.OrderByDescending(m => m.SpotDescription);
                    break;
                default://預設sort by ID
                    spots = _search.sortType == "asc" ? spots.OrderBy(m => m.SpotId) : spots.OrderByDescending(m => m.SpotId);
                    break;
            }

            //分頁
            //total
            int totalCount = spots.Count();
            //each page with number of data
            int pageSize = _search.pagesize ?? 9;
            //current page
            int page = _search.page ?? 1;
            //total page
            int totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            //current data
            spots = spots.Skip((int)((page-1)*pageSize)).Take(pageSize);

            //after data
            spotsPagingDTO spotsPaging = new spotsPagingDTO();
            spotsPaging.totalPages = totalPages;
            spotsPaging.spotsResult = spots.ToList();
            return Json(spotsPaging);
        }

        
        public IActionResult autoComplete(string title)
        {
            var spottitles = _context.Spots.Where(m=>m.SpotTitle.Contains(title))
                .Select(s=> s.SpotTitle).Take(8);
            return Json(spottitles);
        }
    }
           
}
