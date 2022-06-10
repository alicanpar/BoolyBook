
using BoolyBook.Models;
using BoolyBook2.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace BoolyBook2.Web.Areas.Admin.Controllers
{
    public class newCategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public newCategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.categories.ToList());
        }


        public IActionResult Create(int? id)
        {
            var x = _db.categories.FirstOrDefault(x => x.Id == id);
            return View(x);
        }

        [HttpPost]
        public IActionResult Create(Category model)
        {
            var currentRecord = _db.categories.Where(x => x.Id == model.Id).FirstOrDefault();
            if (currentRecord == null) { currentRecord = new Category() { Name = model.Name, DisplayOrder = model.DisplayOrder, CreatedDate = DateTime.Now }; _db.categories.Add(currentRecord); }
            else
            {
                currentRecord.Name = model.Name;
                currentRecord.DisplayOrder = model.DisplayOrder;
                currentRecord.UpdateDate = DateTime.Now;
            }
            _db.SaveChangesAsync();
            //            return RedirectToAction("Create",new {id=currentRecord.Id});
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            var x = _db.categories.FirstOrDefault(x => x.Id == id);
            if (x != null)
            {
                _db.categories.Remove(x);
                _db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
