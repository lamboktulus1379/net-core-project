using System.Linq.Expressions;

namespace TestApplication.Core.Interfaces;

public interface IRepositoryBase<T>
{
    IQueryable<T> FindAll();
    IQueryable<T> FindAllWithRelation(Expression<Func<T, Test>> expression);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}