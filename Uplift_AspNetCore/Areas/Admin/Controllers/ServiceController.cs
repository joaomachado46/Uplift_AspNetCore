using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Uplift.DataAccess.Data;
using Uplift.DataAccess.Data.Repository.IRepos;
using Uplift.DataModels;
using Uplift.DataModels.ViewModels;

namespace Uplift_AspNetCore.Areas.Admin.Controllers
{

    [Area("admin")]
    public class ServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public ServiceVM ServVM { get; set; }

        public ServiceController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult UpSert(int id)
        {
            try
            {
                ServiceVM service = new ServiceVM();
                if (id != 0)
                {
                    service.Service = _unitOfWork.Service.Get(id);
                    service.Categorys = _unitOfWork.Category.GetCategoryListForDropDown();
                    service.Frequencys = _unitOfWork.Frequency.GetFrequencyListForDropDown();
                    return View(service);
                }
                else
                {
                    service.Service = new Service();
                    service.Categorys = _unitOfWork.Category.GetCategoryListForDropDown();
                    service.Frequencys = _unitOfWork.Frequency.GetFrequencyListForDropDown();
                    return View(service);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            //if (ModelState.IsValid)
            //{
            //    string webRootPath = _hostEnvironment.WebRootPath;
            //    var files = HttpContext.Request.Form.Files;
            //    if (ServVM.Service.Id == 0)
            //    {
            //        //New Service
            //        string fileName = Guid.NewGuid().ToString();
            //        var uploads = Path.Combine(webRootPath, @"images\services");
            //        var extension = Path.GetExtension(files[0].FileName);

            //        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
            //        {
            //            files[0].CopyTo(fileStreams);
            //        }
            //        ServVM.Service.ImageUrl = @"\images\services\" + fileName + extension;

            //        _unitOfWork.Service.Add(ServVM.Service);
            //    }
            //    else
            //    {
            //        //Edit Service
            //        var serviceFromDb = _unitOfWork.Service.Get(ServVM.Service.Id);
            //        if (files.Count > 0)
            //        {
            //            string fileName = Guid.NewGuid().ToString();
            //            var uploads = Path.Combine(webRootPath, @"images\services");
            //            var extension_new = Path.GetExtension(files[0].FileName);

            //            var imagePath = Path.Combine(webRootPath, serviceFromDb.ImageUrl.TrimStart('\\'));
            //            if (System.IO.File.Exists(imagePath))
            //            {
            //                System.IO.File.Delete(imagePath);
            //            }

            //            using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension_new), FileMode.Create))
            //            {
            //                files[0].CopyTo(fileStreams);
            //            }
            //            ServVM.Service.ImageUrl = @"\images\services\" + fileName + extension_new;
            //        }
            //        else
            //        {
            //            ServVM.Service.ImageUrl = serviceFromDb.ImageUrl;
            //        }

            //        _unitOfWork.Service.Update(ServVM.Service);
            //    }
            //    _unitOfWork.Save();
            //    return RedirectToAction(nameof(Index));
            //}
            //else
            //{
            //    ServVM.Categorys = _unitOfWork.Category.GetCategoryListForDropDown();
            //    ServVM.Frequencys = _unitOfWork.Frequency.GetFrequencyListForDropDown();
            //    return View(ServVM);
            //}
            if (ModelState.IsValid)
            {
                var WebRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (ServVM.Service.Id == 0)
                {
                    foreach (var file in files)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        var upload = Path.Combine(WebRootPath, @"Images\services\");
                        using (var fileStreams = new FileStream(Path.Combine(upload + fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStreams);
                        }
                        ServVM.Service.ImageUrl = @"\Images\services\" + fileName;
                        _unitOfWork.Service.Add(ServVM.Service);
                        _unitOfWork.Save();
                    }
                }
                else
                {

                    if (files.Count > 0)
                    {
                        foreach (var file in files)
                        {
                            string fileName = Path.GetFileName(file.FileName);
                            var upload = Path.Combine(WebRootPath, @"Images\services\");
                            var extensionFile = Path.GetExtension(file.FileName);
                            using (var fileStreams = new FileStream(Path.Combine(upload + fileName), FileMode.Create))
                            {
                                file.CopyTo(fileStreams);
                            }
                            ServVM.Service.ImageUrl = @"\Images\services\" + fileName;
                        }
                    }
                    _context.Services.Update(ServVM.Service);
                    _unitOfWork.Save();
                }
                return RedirectToAction(nameof(Index));
            }
            ServiceVM serviceVM = new ServiceVM();
            serviceVM.Service = new Service();
            serviceVM.Categorys = _unitOfWork.Category.GetCategoryListForDropDown();
            serviceVM.Frequencys = _unitOfWork.Frequency.GetFrequencyListForDropDown();
            return View(serviceVM);
        }


        #region API CALLS Service
        [HttpGet]
        public IActionResult GetAll()
        {
            //includeProperties, serve para carregar também a conecção com as class que tem algum relacionamento.
            return Json(new { data = _unitOfWork.Service.GetAll(includeProperties: "Category,Frequency") });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id != 0)
            {
                var webRootPath = _hostEnvironment.WebRootPath;
                var path = _unitOfWork.Service.Get(id);
                var imagePath = Path.Combine(webRootPath, path.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                _unitOfWork.Service.Remove(id);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Delete successful." });
            }
            else
            {
                return Json(new { success = false, message = "Error while deleting." });
            }
        }
        #endregion
    }
}
