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
            Frequency = new FrequencyRepository(context);
            Service = new ServiceRepository(context);
            OrderHeader = new OrderHeaderRepository(context);
            OrderDetails = new OrderDetailsRepository(context);
            User = new UserRepository(context);
        }
        //TODAS AS CLASS COM REPOSITORY TEM QUE ENTRAR UMA REFERENCIA AQUI E DEPOIS NO CONSTRUTOR RECEVER UMA NOVA INSTANCIA DO SEU REPOSITORIO.
        public ICategoryRepository Category { get; private set; }
        public IFrequencyRepository Frequency { get; private set; }
        public IServiceRepository Service { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IOrderDetailsRepository OrderDetails { get; private set; }
        public IUserRepository User { get; private set; }

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
