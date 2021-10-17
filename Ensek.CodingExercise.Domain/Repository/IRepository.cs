using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ensek.CodingExercise.Domain.Entities;

namespace Ensek.CodingExercise.Domain.Repository
{
    public interface IRepository<T> where T : IEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IList<T>> GetAllAsync();
        Task<bool> AddAsync(T entity);
    }
}
