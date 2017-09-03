using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CheckoutCom.ShoppingList.Models;

namespace CheckoutCom.ShoppingList.DataAccess.Base
{
    public interface IRepository<T> where T : Entity
    {
        T GetById(int id);
        IEnumerable<T> List();
        IEnumerable<T> List(Expression<Func<T, bool>> predicate);
        void Insert(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
