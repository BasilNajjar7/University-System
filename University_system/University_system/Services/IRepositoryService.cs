using Microsoft.AspNetCore.Mvc;
using University_system.Model;

namespace University_system.Services
{
    public interface IRepositoryService<T>where T : class
    {
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        T Update(Guid id,T entity);
        IEnumerable<Student> GetByYear(int year);
        T Add(T entity);
        T Delete(Guid id);
    }
}