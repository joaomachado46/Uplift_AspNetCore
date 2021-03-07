using System;
using System.Collections.Generic;
using System.Text;
using Uplift.DataAccess.Data.Repository.IRepos;

namespace Uplift.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(context);
        }
        //ODAS AS CLASS COM REPOSITORY TEM QUE ENTRAR UMA REFERENCIA AQUI E DEPOIS NO CONSTRUTOR RECEVER UMA NOVA INSTANCIA DO SEU REPOSITORIO.
        public ICategoryRepository Category { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        //METODO PARA UNICAMENTE SALVAS OS DADOS NA BD
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
