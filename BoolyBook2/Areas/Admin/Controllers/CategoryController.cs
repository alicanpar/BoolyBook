using BoolyBook.DataAccess.IRepository;
using BoolyBook.Models;
using BoolyBook2.DataAccess;
using BoolyBook2.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BoolyBook2.Web.Areas.Admin.Controllers;
[AllowAnonymous]
[Area("Admin")]
[Authorize(Roles =SD.Role_Admin)]
public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
        return View(objCategoryList);
    }
    //GET
    public IActionResult Create()
    {
        return View();
    }
    //Post
    [HttpPost]
    [ValidateAntiForgeryToken] //kimlik doğrulayıcı
    public IActionResult Create(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("CustomError", "Aynı veri girilemez.");
            return View(obj);
        }
        if (ModelState.IsValid)
        {
            obj.CreatedDate = DateTime.Now;
            _unitOfWork.Category.Add(obj); //Veri tabanına kayıt
            _unitOfWork.Save(); //Değişiklikleri kaydeder
            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }
    //GET
    public IActionResult Edit(int? id) //Id ile kategori ayrıntıları alınır görüüntülenir
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        //var categoryFromdb = _db.categories.Find(id);//kimliğe göre kategoriyi bulacak ve okunabilir olarak atayacak 
        var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
        //var categoryFromSingle = _db.categories.SingleOrDefault(u => u.Id == id);
        if (categoryFromDbFirst == null)
        {
            return NotFound();
        }
        return View(categoryFromDbFirst);
    }
    //Post
    [HttpPost]
    [ValidateAntiForgeryToken] //kimlik doğrulayıcı
    public IActionResult Edit(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("CustomError", "Aynı veri girilemez.");
        }
        if (ModelState.IsValid)
        {
            //var entity = _db.GetFirstOrDefault(f => f.Id ==obj.Id);
            //if(entity==null)
            //{                    
            //    throw new Exception();
            //}
            //entity.Name = obj.Name;
            //entity.DisplayOrder = obj.DisplayOrder; 
            //entity.UpdateDate = DateTime.Now;                
            _unitOfWork.Category.Update(obj); //Veri tabanına kayıt                
            _unitOfWork.Save(); //Değişiklikleri kaydeder
            TempData["success"] = "Category edited successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }
    //GET
    public IActionResult Delete(int? id) //Id ile kategori ayrıntıları alınır görüüntülenir
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        //var categoryFromdb = _db.categories.Find(id);//kimliğe göre kategoriyi bulacak ve okunabilir olarak atayacak 
        var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
        //var categoryFromSingle = _db.categories.SingleOrDefault(u => u.Id == id);
        if (categoryFromDbFirst == null)
        {
            return NotFound();
        }
        return View(categoryFromDbFirst);
    }
    //Post
    [HttpPost]
    [ValidateAntiForgeryToken] //kimlik doğrulayıcı
    public IActionResult DeletePOST(int? id)
    {
        var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
        if (obj == null)
        {
            return NotFound();
        }
        _unitOfWork.Category.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");

    }

}
