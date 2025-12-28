using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using University_system.Data;
using University_system.Model;

namespace University_system.Services
{
    public class RepositoryService<T> : IRepositoryService<T> where T : class
    {
        protected ApplicationDbContext _context;
        public RepositoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }
        public T Delete(Guid id)
        {
            var result = _context.Set<T>().Find(id);
            
            if(result!=null)
            {
                _context.Set<T>().Remove(result);
                _context.SaveChanges();
            }

            return result;
        }
        public IEnumerable<T> GetAll() => _context.Set<T>().ToList();
        public T GetById(Guid id) => _context.Set<T>().Find(id);
        public IEnumerable<Student> GetByYear(int year) => 
            _context.Set<Student>().Where(b=>b.Year_of_registration.Equals(year)).ToList();
        public T Update(Guid id,T entity)
        {
            var res = _context.Set<T>().Find(id);

            res = entity;
            _context.SaveChanges();

            return res;
        }
    }
}
