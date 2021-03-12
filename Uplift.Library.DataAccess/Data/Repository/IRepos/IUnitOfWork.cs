using System;
using System.Collections.Generic;
using System.Text;

namespace Uplift.DataAccess.Data.Repository.IRepos
{
    public interface IUnitOfWork : IDisposable
    {
        //TODAS AS CLASS QUE VAI TER UM REPOSITORY, TEM QUE SER DECLARADAS AQUI PARA PODEREM SER USADAS NOS CONTROLLERS E ACESSAR DADOS.
        ICategoryRepository Category { get; }
        IFrequencyRepository Frequency { get; }
        IServiceRepository Service { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailsRepository OrderDetails { get; }
        IUserRepository User { get; }
        void Save();

    }
}
