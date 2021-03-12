using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uplift.DataAccess.Data.Repository.IRepos;
using Uplift.Utility;

namespace Uplift_AspNetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }


        #region ApiCalls

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.OrderHeader.GetAll() });
        }

        [HttpGet]
        public IActionResult GetAllAproved()
        {
            var resultFinal = "";
            var result = _unitOfWork.OrderHeader.GetAll();
            foreach (var item in result)
            {
                if (item.Status == SD.Pending)
                {
                    resultFinal += item;
                }
            }
            return Json(new { data = resultFinal });
        }

        [HttpGet]
        public IActionResult GetAllPending()
        {
            var resultFinal = "";
            var result = _unitOfWork.OrderHeader.GetAll();
            foreach (var item in result)
            {
                if(item.Status == SD.Aproved)
                {
                    resultFinal += item;
                }
            }
            return Json(new { data = resultFinal });
        }


        #endregion
    }
}
