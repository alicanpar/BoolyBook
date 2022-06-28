using BoolyBook.DataAccess.IRepository;
using BoolyBook.Models;
using BoolyBook.Models.ViewModels;
using BoolyBook2.DataAccess;
using BoolyBook2.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace BoolyBook2.Web.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]

public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _hostEnvironment;
    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _hostEnvironment = hostEnvironment;
    }

    public IActionResult Index()
    {
        return View();
    }
    //GET
    public IActionResult Upsert(int? id) //Id ile kategori ayrıntıları alınır görüüntülenir
    {
        ProductVM productVM = new()
        {
            Product = new(),
            CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }),
            CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }),
        };
        if (id == null || id == 0)
        {
            //create product
            //ViewBag.CategoryList=CategoryList;
            //ViewData["CoverTypeList"] = CoverTypeList;
            return View(productVM);
        }
        else
        {
            productVM.Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            return View(productVM);
            //update product
        }
    }
    //Post
    [HttpPost]
    [ValidateAntiForgeryToken] //kimlik doğrulayıcı
    public IActionResult Upsert(ProductVM obj, IFormFile file)
    {
        if (ModelState.IsValid)
        {
            string wwwRoothPath = _hostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRoothPath, @"images\products");

                if (obj.Product.ImageUrl != null)
                {
                    var oldImagePath = Path.Combine(wwwRoothPath, obj.Product.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                var extension = Path.GetExtension(file.FileName);
                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }
                obj.Product.ImageUrl = @"\images\products\" + fileName + extension;
            }
            if (obj.Product.Id == 0)
            {
                _unitOfWork.Product.Add(obj.Product); //Veri tabanına kayıt
            }
            else
            {
                _unitOfWork.Product.Update(obj.Product);
            }
            //var entity = _db.GetFirstOrDefault(f => f.Id ==obj.Id);
            //if(entity==null)
            //{                    
            //    throw new Exception();
            //}
            //entity.Name = obj.Name;
            //entity.DisplayOrder = obj.DisplayOrder; 
            //entity.UpdateDate = DateTime.Now;                

            _unitOfWork.Save(); //Değişiklikleri kaydeder
            TempData["success"] = "Product Create successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }
    #region API CALLS
    [HttpGet]
    public IActionResult GetAll()
    {
        var productList = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
        return Json(new { data = productList });
    }

    //Post
    [HttpDelete]
    //[ValidateAntiForgeryToken] //kimlik doğrulayıcı
    public IActionResult Delete(int? id)
    {
        var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
        if (obj == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }
        var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
        if (System.IO.File.Exists(oldImagePath))
        {
            System.IO.File.Delete(oldImagePath);
        }
        _unitOfWork.Product.Remove(obj);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Deleted successful" });
    }
    #endregion


}
