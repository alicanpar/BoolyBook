using BoolyBook.Models;
using BoolyBook2.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BoolyBook2.Web.Areas.Admin.Controllers
{
    public class FormsController : Controller
    {
        private readonly ILogger<FormsController> _logger;
        private readonly ApplicationDbContext _db;


        public FormsController(ILogger<FormsController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreatePartial(int id)
        {
            var model = new Category();
            return PartialView("Partial_List_Create", model);
        }
        public IActionResult EditPartial(int id)
        {

            IEnumerable<Category> model = _db.categories;

            return PartialView("Partial_List_Edit", model);
        }
        //public IActionResult EditPartial_editok(int id)
        //{

        //  IEnumerable<Category> model = _db.categories;

        //return PartialView("Partial_List_Editok", model);
        //}
        public IActionResult EditPartial_editok(int? catId) //Id ile kategori ayrıntıları alınır görüüntülenir
        {
            IEnumerable<Category> model = _db.categories;
            if (catId == null || catId == 0)
            {
                return NotFound();
            }
            var categoryFromdb = _db.categories.Find(catId);//kimliğe göre kategoriyi bulacak ve okunabilir olarak atayacak 
            if (categoryFromdb == null)
            {
                return NotFound();
            }
            return PartialView("Partial_List_Editok", model);
        }
    }
}