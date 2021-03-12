using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uplift.DataAccess.Data.Repository.IRepos;
using Uplift.DataModels;

namespace Uplift.DataAccess.Data.Repository
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        private readonly ApplicationDbContext _db;

        public ServiceRepository(ApplicationDbContext context) : base(context)
        {
            _db = context;
        }

        public void Update(Service service)
        {
            try
            {
                var objFromDb = _db.Services.FirstOrDefault(s => s.Id == service.Id);

                objFromDb.Name = service.Name;
                objFromDb.LongDesc = service.LongDesc;
                objFromDb.Price = service.Price;
                objFromDb.ImageUrl = service.ImageUrl;
                objFromDb.FrequencyId = service.FrequencyId;
                objFromDb.CategoryId = service.CategoryId;

                _db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
