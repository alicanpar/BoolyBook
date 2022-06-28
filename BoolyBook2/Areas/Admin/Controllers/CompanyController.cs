using BoolyBook.DataAccess.IRepository;
using BoolyBook.Models;
using BoolyBook.Models.ViewModels;
using BoolyBook2.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace BoolyBook2.Web.Areas.Admin.Controllers;

[AllowAnonymous]
[Area("Admin")]
public class CompanyController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public CompanyController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        return View();
    }
    //GET
    public IActionResult Upsert(int? id) //Id ile kategori ayrıntıları alınır görüüntülenir
    {
        Company company = new();
       
        if (id == null || id == 0)
        {           
            return View(company);
        }
        else
        {
            company = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
            return View(company);
            //update product
        }
    }
    //Post
    [HttpPost]
    [ValidateAntiForgeryToken] //kimlik doğrulayıcı
    public IActionResult Upsert(Company obj)
    {
        if (ModelState.IsValid)
        {        
            if (obj.Id == 0)
            {
                _unitOfWork.Company.Add(obj); //Veri tabanına kayıt
                TempData["success"] = "Company Create successfully";
            }
            else
            {
                _unitOfWork.Company.Update(obj);
                TempData["success"] = "Company Updated successfully";

            }
            _unitOfWork.Save(); //Değişiklikleri kaydeder
            return RedirectToAction("Index");
        }
        return View(obj);
    }
    #region API CALLS
    [HttpGet]
    public IActionResult GetAll()
    {
        var companyList = _unitOfWork.Company.GetAll();
        return Json(new { data = companyList });
    }

    //Post
    [HttpDelete]
    //[ValidateAntiForgeryToken] //kimlik doğrulayıcı
    public IActionResult Delete(int? id)
    {
        var obj = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
        if (obj == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }        
        _unitOfWork.Company.Remove(obj);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Deleted successful" });
    }
    #endregion


}
