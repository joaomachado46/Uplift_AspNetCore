using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uplift.DataAccess.Data.Repository.IRepos;
using Uplift.DataModels;

namespace Uplift.DataAccess.Data.Repository
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base (context)
        {
            _context = context;
        }

        public void LockUser(string id)
        {
            var user = _context.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.LockoutEnd = DateTime.Now.AddYears(100);
                _context.ApplicationUsers.Update(user);
                _context.SaveChanges();
            }
        }

        public void UnLockUser(string id)
        {
            var user = _context.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.LockoutEnd = null;
                _context.ApplicationUsers.Update(user);
                _context.SaveChanges();
            }
        }
    }
}
