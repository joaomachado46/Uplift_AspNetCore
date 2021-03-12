using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uplift.DataAccess.Data.Repository.IRepos;
using Uplift.DataModels;
using Uplift.DataModels.ViewModels;
using Uplift.Utility;
using Uplift_AspNetCore.Extensions;

namespace Uplift_AspNetCore.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //USAMOS ISTO PARA FAZER O BIND DA PROPRIEDADE NOS PARAMETROS DAS ACTIONS

        [BindProperty]
        public CartVM cartVM { get; set; }

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //PRECISAMOS TAMBÉM ESTANCIAR O BIND NO CONSTRUTOR
            cartVM = new CartVM()
            {
                OrderHeaders = new OrderHeader(),
                ServiceList = new List<Service>(),
            };
        }

        public IActionResult Index()
        {
            var resultListCart = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
            var result = _unitOfWork.Service.GetAll(includeProperties: "Frequency,Category");

            foreach (var item in result)
            {
                if (resultListCart.Contains(item.Id))
                {
                    cartVM.ServiceList.Add(item);
                }
            }
            return View(cartVM);
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            var result = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
            if (result.Contains(id))
            {
                result.Remove(id);
                HttpContext.Session.SetObject(SD.SessionCart, result);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Summary()
        {
            var resultListCart = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
            var result = _unitOfWork.Service.GetAll(includeProperties: "Frequency,Category");
            cartVM.ServiceList = new List<Service>();
            foreach (var item in result)
            {
                if (resultListCart.Contains(item.Id))
                {
                    cartVM.ServiceList.Add(item);
                }
            }
            return View(cartVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public IActionResult SummaryPost()
        {
            var resultListCart = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
            var result = _unitOfWork.Service.GetAll(includeProperties: "Frequency,Category");
            cartVM.ServiceList = new List<Service>();
            foreach (var item in result)
            {
                if (resultListCart.Contains(item.Id))
                {
                    cartVM.ServiceList.Add(item);
                }
            }
            CartVM cartVMNew = new CartVM();
            if (!ModelState.IsValid)
            {
                return View(cartVM);
            }
            else
            {
                cartVMNew.OrderHeaders = new OrderHeader();
                cartVMNew.OrderHeaders.Name = cartVM.OrderHeaders.Name;
                cartVMNew.OrderHeaders.PhoneNumber = cartVM.OrderHeaders.PhoneNumber;
                cartVMNew.OrderHeaders.Address = cartVM.OrderHeaders.Address;
                cartVMNew.OrderHeaders.City = cartVM.OrderHeaders.City;
                cartVMNew.OrderHeaders.ZipCode = cartVM.OrderHeaders.ZipCode;
                cartVMNew.OrderHeaders.Email = cartVM.OrderHeaders.Email;
                cartVMNew.OrderHeaders.OrderDate = DateTime.Now;
                cartVMNew.OrderHeaders.ServiceCount = cartVM.ServiceList.Count;
                cartVMNew.OrderHeaders.Status = SD.Submitted;
                _unitOfWork.OrderHeader.Add(cartVMNew.OrderHeaders);
                _unitOfWork.Save();
            }

            foreach (var item in cartVM.ServiceList)
            {
                OrderDetails orderDetails = new OrderDetails()
                {
                    OrderHeaderId = cartVMNew.OrderHeaders.Id,
                    ServiceId = item.Id,
                    ServiceName = item.Name,
                    Price = item.Price,
                };
                _unitOfWork.OrderDetails.Add(orderDetails);

            }
            _unitOfWork.Save();
            HttpContext.Session.SetObject(SD.SessionCart, new List<int>());
            return RedirectToAction("OrderConfirmation", new { id = cartVMNew.OrderHeaders.Id });
        }

        public IActionResult OrderConfirmation(int id)
        { 
            return View(id);
        }
    }
}
