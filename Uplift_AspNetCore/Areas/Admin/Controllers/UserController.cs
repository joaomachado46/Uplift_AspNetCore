using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Uplift.DataAccess.Data.Repository.IRepos;
using Uplift.DataModels;
using Uplift.Utility;

namespace Uplift_AspNetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            ////ESTA OPÇÃO VAI BUSCAR O ID DO CLIENTE LOGADO E DEPOIS PODEMOS FAZER A COMPARAÇÃO COM A LISTA DA BD
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            //ESTA IPÇÃO VAI BUSCAR O USERNAME E FAZ A COMPARAÇÃO PELO USARNAME
            List<ApplicationUser> listUser = new List<ApplicationUser>();
            if (User.Identity.IsAuthenticated)
            {
                var resultSearchUser = _unitOfWork.User.GetAll();

                foreach (var item in resultSearchUser)
                {
                    if (item.UserName != User.Identity.Name)
                    {
                        listUser.Add(item);
                    }
                };
            }
            IEnumerable<ApplicationUser> user = listUser;
            return View(user);
        }

        public IActionResult Lock(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.Role);

                if (claim.Value.ToString() == SD.Admin)
                {
                    _unitOfWork.User.LockUser(id);
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult UnLock(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.Role);

                if (claim.Value.ToString() == SD.Admin)
                {
                    _unitOfWork.User.UnLockUser(id);
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
