using BoolyBook.DataAccess.IRepository;
using BoolyBook.Models;
using BoolyBook2.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace BoolyBook2.Web.Areas.Admin.Controllers;

public class CoverTypeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public CoverTypeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
        return View(objCoverTypeList);
    }
    //GET
    public IActionResult Create()
    {
        return View();
    }
    //Post
    [HttpPost]
    [ValidateAntiForgeryToken] //kimlik doğrulayıcı
    public IActionResult Create(CoverType obj)
    {
        
        if (ModelState.IsValid)
        {
            //obj.CreatedDate = DateTime.Now;
            _unitOfWork.CoverType.Add(obj); //Veri tabanına kayıt
            _unitOfWork.Save(); //Değişiklikleri kaydeder
            TempData["success"] = "CoverType created successfully";
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
        //var categoryFromdb = _db.CoverType.Find(id);//kimliğe göre kategoriyi bulacak ve okunabilir olarak atayacak 
        var CoverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
        //var categoryFromSingle = _db.CoverType.SingleOrDefault(u => u.Id == id);
        if (CoverTypeFromDbFirst == null)
        {
            return NotFound();
        }
        return View(CoverTypeFromDbFirst);
    }
    //Post
    [HttpPost]
    [ValidateAntiForgeryToken] //kimlik doğrulayıcı
    public IActionResult Edit(CoverType obj)
    {        
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
            _unitOfWork.CoverType.Update(obj); //Veri tabanına kayıt                
            _unitOfWork.Save(); //Değişiklikleri kaydeder
            TempData["success"] = "CoverType edited successfully";
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
        var CoverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
        //var categoryFromSingle = _db.categories.SingleOrDefault(u => u.Id == id);
        if (CoverTypeFromDbFirst == null)
        {
            return NotFound();
        }
        return View(CoverTypeFromDbFirst);
    }
    //Post
    [HttpPost]
    [ValidateAntiForgeryToken] //kimlik doğrulayıcı
    public IActionResult DeletePOST(int? id)
    {
        var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
        if (obj == null)
        {
            return NotFound();
        }
        _unitOfWork.CoverType.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "CoverType deleted successfully";
        return RedirectToAction("Index");

    }

}
