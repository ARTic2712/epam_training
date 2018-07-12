using SalesSystem.Entities;
using System;
using System.Collections.Generic;

namespace SalesSystem
{
    public interface IRepository<T> where T: class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        void Create(T item);
        void CreateUser(ApplicationUser user, string password);

        void Update(T item);
        void Delete(int id);
    }
}
