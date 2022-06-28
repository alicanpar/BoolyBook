using BoolyBook.DataAccess.IRepository;
using BoolyBook.Models;
using BoolyBook.Models.ViewModels;
using BoolyBook2.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace BoolyBook2.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public OrderVM OrderVM { get; set; }
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int orderId)
        {
            OrderVM = new OrderVM()
            {
                OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == orderId, includeProperties: "ApplicationUser"),
                OrderDetail = _unitOfWork.OrderDetail.GetAll(u => u.OrderId == orderId, includeProperties: "Product"),

            };
            return View(OrderVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateOrderDetail()
        {
            var orderHeaderFomDb = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == OrderVM.OrderHeader.Id);
            orderHeaderFomDb.Name = OrderVM.OrderHeader.Name;
            orderHeaderFomDb.PhoneNumber = OrderVM.OrderHeader.PhoneNumber;
            orderHeaderFomDb.StreetAddress = OrderVM.OrderHeader.StreetAddress;
            orderHeaderFomDb.City = OrderVM.OrderHeader.City;
            orderHeaderFomDb.State = OrderVM.OrderHeader.State;
            orderHeaderFomDb.PostalCode = OrderVM.OrderHeader.PostalCode;
            if(OrderVM.OrderHeader.Carrier!=null)
            {
                orderHeaderFomDb.Carrier = OrderVM.OrderHeader.Carrier;
            }
            if(OrderVM.OrderHeader.TrackingNumber!=null)
            {
                orderHeaderFomDb.TrackingNumber = OrderVM.OrderHeader.TrackingNumber;
            }
            //_unitOfWork.OrderHeader.Update(orderHeaderFomDb);
            _unitOfWork.Save();
            TempData["Success"] = "Order Detail başarıyla güncellendi";
            return RedirectToAction("Details", "Order", new {orderId=orderHeaderFomDb.Id});
        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll(string status)
        {
            IEnumerable<OrderHeader> orderHeaders;
            if (User.IsInRole(SD.Role_Admin) || User.IsInRole(SD.Role_Employee))
            {
                orderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser");

            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                orderHeaders = _unitOfWork.OrderHeader.GetAll(u => u.ApplicationUserId == claim.Value, includeProperties: "ApplicationUser");

            }
            switch (status)
            {
                case "pending":
                    orderHeaders = orderHeaders.Where(u => u.PaymentStatus == SD.PaymentStatusDelayedPayment);
                    break;
                case "inprocess":
                    orderHeaders = orderHeaders.Where(u => u.OrderStatus == SD.StatusInProcess);
                    break;
                case "completed":
                    orderHeaders = orderHeaders.Where(u => u.OrderStatus == SD.StatusShipped);
                    break;
                case "approved":
                    orderHeaders = orderHeaders.Where(u => u.OrderStatus == SD.StatusApproved);
                    break;
                default:
                    break;

            }
            return Json(new { data = orderHeaders });
        }
        #endregion
    }
}
