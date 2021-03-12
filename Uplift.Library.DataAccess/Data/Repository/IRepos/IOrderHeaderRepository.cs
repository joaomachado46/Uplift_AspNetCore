using Uplift.DataModels;

namespace Uplift.DataAccess.Data.Repository.IRepos
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        public void UpdateStatus(int id, string newStatus);
    }
}
