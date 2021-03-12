using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uplift.DataAccess.Data.Repository.IRepos;
using Uplift.DataModels;

namespace Uplift_AspNetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FrequencyController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public FrequencyController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UpSert(int id)
        {
            try
            {
                Frequency frequency = new Frequency();
                if (id == 0)
                {
                    return View(frequency);
                }
                else
                {
                    var result = unitOfWork.Frequency.Get(id);
                    if (result == null)
                    {
                        return View(frequency);
                    }
                    else
                    {
                        return View(result);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Upsert([FromForm] Frequency frequency)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (frequency.Id != 0)
                    {
                        unitOfWork.Frequency.Update(frequency);
                        unitOfWork.Save();
                    }else
                    {
                        unitOfWork.Frequency.Add(frequency);
                        unitOfWork.Save();
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(frequency);
            }
            catch (Exception)
            {
                throw;
            }

        }


        #region APIFrequency

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Json(new { data = unitOfWork.Frequency.GetAll() });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id.Equals(null))
            {
                return BadRequest();
            }

            var result = unitOfWork.Frequency.Get(id);
            if (result == null)
            {
                return Json(new { success = false, message = "Bad request to delete" });
            }
            else
            {
                unitOfWork.Frequency.Remove(result);
                unitOfWork.Save();
                return Json(new { success = true, message = "success in delete de frequency" });
            }
        }
        #endregion
    }
}

