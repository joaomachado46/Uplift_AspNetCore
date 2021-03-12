using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uplift.DataAccess.Data.Repository.IRepos;
using Uplift.DataModels;

namespace Uplift.DataAccess.Data.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderHeaderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void UpdateStatus(int id, string newStatus)
        {
            var resultSearch = _context.OrderHeaders.FirstOrDefault(i => i.Id == id);
            if (resultSearch != null)
            {
                resultSearch.Status = newStatus;
                _context.OrderHeaders.Update(resultSearch);
                _context.SaveChanges();
            }
        }
    }
}
