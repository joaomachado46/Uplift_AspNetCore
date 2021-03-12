using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uplift.DataModels;

namespace Uplift.DataAccess.Data.Repository.IRepos
{
    public interface IFrequencyRepository : IRepository<Frequency>
    {
        void Update(Frequency frequency);
        IEnumerable<SelectListItem> GetFrequencyListForDropDown();
    }
}
