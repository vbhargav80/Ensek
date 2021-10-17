using System.Collections.Generic;
using System.Threading.Tasks;
using Ensek.CodingExercise.Domain.Entities;
using Ensek.CodingExercise.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Ensek.CodingExercise.Infrastructure.Repository
{
    public class SqlRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly EnsekDbContext _dbContext;

        public SqlRepository(EnsekDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<bool> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
