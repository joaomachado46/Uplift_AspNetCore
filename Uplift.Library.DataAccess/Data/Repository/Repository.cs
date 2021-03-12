using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Uplift.DataAccess.Data.Repository.IRepos;
using Uplift.DataModels;

namespace Uplift.DataAccess.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        protected readonly DbContext _context;
        //ESTA PROP VAI RECEBER O CONTEXTO CORRENTE, TEM QUE SER INICIADA NO CONTRUTOR
        internal DbSet<T> dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }
        //ADICIONAR UM OBJETO NA BD
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }
        //PROCURAR UM OBJETO NA BD POR ID
        public T Get(int id)
        {
            return dbSet.Find(id);
        }
        //RETORNA TODOS OS DADOS DO OBJETO, PASSA COMO PARAMETRO UM FILTRO, ORDER BY E PROPRIEDADES
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var propertie in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(propertie);
                }
            }
            if(orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        //RETORNA O PRIMEIRO RESULTADO OBJETIVO COM OS PARAMETROS ENVIADOS
        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var propertie in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(propertie);
                }
            }
            return query.FirstOrDefault();
        }

        //REMOVER POR CONSULTA DE ID
        public void Remove(int id)
        {
            T entityForRemove = dbSet.Find(id);
            Remove(entityForRemove);
        }
        //REMOVER PASSANDO UM OBJETO
        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
