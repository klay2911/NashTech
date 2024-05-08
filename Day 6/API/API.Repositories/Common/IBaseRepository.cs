// <copyright file="IBaseRepository.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
// </copyright>

using System.Linq.Expressions;

namespace API.Repositories.Common
{
    public interface IBaseRepository<T>
    {
        T Add(T entity);
        
        T Update(T entity);
        
        Task<T> GetAsync(Guid id);
        
        Task<IEnumerable<T>> GetAllAsync();
        
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        
        T Delete(T entity);
    }
}