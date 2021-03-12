using Uplift.DataModels;

namespace Uplift.DataAccess.Data.Repository.IRepos
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        void LockUser(string id);
        void UnLockUser(string id);
    }
}
