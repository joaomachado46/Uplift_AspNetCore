using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uplift.DataAccess.Data.Repository.IRepos;
using Uplift.DataModels;

namespace Uplift.DataAccess.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetCategoryListForDropDown()
        {
            return _context.Categorys.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public void Update(Category category)
        {
            var ObjFromDb = _context.Categorys.FirstOrDefault(i => i.Id == category.Id);

            ObjFromDb.Name = category.Name;
            ObjFromDb.DisplayOrder = category.DisplayOrder;

            _context.SaveChanges();
        }
    }
}
