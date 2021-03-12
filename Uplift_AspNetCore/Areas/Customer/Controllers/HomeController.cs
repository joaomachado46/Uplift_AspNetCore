    using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Uplift.DataAccess.Data.Repository.IRepos;
using Uplift.DataModels;
using Uplift.DataModels.ViewModels;
using Uplift.Utility;
using Uplift_AspNetCore.Extensions;
using Uplift_AspNetCore.Models;

namespace Uplift_AspNetCore.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeVM homeVM;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            homeVM = new HomeVM();
            homeVM.CategoryList = _unitOfWork.Category.GetAll();
            homeVM.SeviceList = _unitOfWork.Service.GetAll(includeProperties: "Frequency");
            return View(homeVM);
        }

        public IActionResult Details(int id)
        {
            Service finalResult = null;
            var result = _unitOfWork.Service.GetAll(includeProperties: "Category,Frequency");
            foreach (var item in result)
            {
                if(item.Id == id)
                {
                    finalResult = item;
                    return View(finalResult);
                }
            }
            return View();
        }

        //METODO PARA ADICIONAR UM ARTIGO EM SESSION/PARA DEPOIS SER ADICIONADO A PAGINA DE CART
        public IActionResult AddCart(int ServiceId)
        {
            //CONFIRMA SE JA EXISTEM DADOS NA KEY
            List<int> sessionList = new List<int>();
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SD.SessionCart)))
            {
                sessionList.Add(ServiceId);
                HttpContext.Session.SetObject(SD.SessionCart, sessionList);
            }
            //SE JA EXISTIRAM DADOS CONFIRMA SE AQUELE DADO JA EXISTE/ SE NÃO ADICIONA NA KEY
            else
            {
               sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
                if (!sessionList.Contains(ServiceId))
                {
                    sessionList.Add(ServiceId);
                    HttpContext.Session.SetObject(SD.SessionCart, sessionList);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
