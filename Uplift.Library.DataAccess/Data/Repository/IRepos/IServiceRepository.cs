using Uplift.DataModels;

namespace Uplift.DataAccess.Data.Repository.IRepos
{
    public interface IServiceRepository : IRepository<Service>
    {
        void Update(Service service);
    }
}
